using UnityEngine;
using VContainer;

public class EnemyController : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private string colorPropertyName = "_BaseColor";
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hitFlashColor = Color.red;
    [SerializeField] private float hitFlashDuration = 0.08f;

    [Header("Hit Reaction")]
    [SerializeField] private float knockbackForce = 3.5f;
    [SerializeField] private float knockbackDamping = 14f;
    
    [Header("Separation")]
    [SerializeField] private float separationRadius = 1.2f;
    [SerializeField] private float separationForce = 2.0f;

    private Transform _playerTarget;
    private EnemyRegistry _enemyRegistry;
    private EnemyPool _enemyPool;

    private float _moveSpeed;
    private int _maxHp;
    private int _currentHp;
    private bool _isActive;

    private Vector3 _knockbackVelocity;
    private float _hitFlashTimer;

    private MaterialPropertyBlock _propertyBlock;
    private int _colorPropertyId;

    public bool IsAlive => _isActive;
    public Vector3 Position => transform.position;

    [Inject]
    public void Construct(EnemyRegistry enemyRegistry, EnemyPool enemyPool)
    {
        _enemyRegistry = enemyRegistry;
        _enemyPool = enemyPool;
    }

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _colorPropertyId = Shader.PropertyToID(colorPropertyName);

        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        ApplyColor(defaultColor);
    }

    public void Spawn(Transform playerTarget, Vector3 position, float moveSpeed, int hp)
    {
        _playerTarget = playerTarget;
        _moveSpeed = moveSpeed;
        _maxHp = hp;
        _currentHp = hp;
        _isActive = true;

        _knockbackVelocity = Vector3.zero;
        _hitFlashTimer = 0f;

        transform.position = position;
        ApplyColor(defaultColor);

        gameObject.SetActive(true);
        _enemyRegistry.Register(this);
    }

    private void Update()
    {
        if (!_isActive)
            return;
        UpdateHitFlash();
        UpdateMovement();
        ApplySeparation();
    }
    
    private void ApplySeparation()
    {
        if (_enemyRegistry == null)
            return;
        var enemies = _enemyRegistry.ActiveEnemies;

        Vector3 currentPos = transform.position;
        Vector3 push = Vector3.zero;
        float radiusSqr = separationRadius * separationRadius;
        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyController other = enemies[i];
            if (other == this || other == null || !other.IsAlive)
                continue;

            Vector3 diff = currentPos - other.Position;
            diff.y = 0f;
            float sqrDist = diff.sqrMagnitude;

            if (sqrDist > 0f && sqrDist < radiusSqr)
            {
                float dist = Mathf.Sqrt(sqrDist);
                Vector3 dir = diff / dist;
                float strength = 1f - (dist / separationRadius);
                push += dir * strength;
            }
        }

        transform.position += push * separationForce * Time.deltaTime;
    }

    private void UpdateMovement()
    {
        Vector3 totalMove = Vector3.zero;

        if (_playerTarget != null)
        {
            Vector3 direction = _playerTarget.position - transform.position;
            direction.y = 0f;

            float sqrMagnitude = direction.sqrMagnitude;
            if (sqrMagnitude > 0.0001f)
            {
                direction /= Mathf.Sqrt(sqrMagnitude);

                totalMove += direction * (_moveSpeed * Time.deltaTime);
                if (_knockbackVelocity.sqrMagnitude < 0.01f)
                {
                    transform.forward = direction;
                }
            }
        }

        if (_knockbackVelocity.sqrMagnitude > 0.0001f)
        {
            totalMove += _knockbackVelocity * Time.deltaTime;
            _knockbackVelocity = Vector3.Lerp(
                _knockbackVelocity,
                Vector3.zero,
                knockbackDamping * Time.deltaTime);
        }

        transform.position += totalMove;
    }

    private void UpdateHitFlash()
    {
        if (_hitFlashTimer <= 0f)
            return;

        _hitFlashTimer -= Time.deltaTime;
        if (_hitFlashTimer <= 0f)
        {
            ApplyColor(defaultColor);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (!_isActive)
            return;

        _currentHp -= damage;
        TriggerHitFlash();
        ApplyKnockback();
        if (_currentHp <= 0)
        {
            Despawn();
        }
    }

    private void TriggerHitFlash()
    {
        _hitFlashTimer = hitFlashDuration;
        ApplyColor(hitFlashColor);
    }

    private void ApplyKnockback()
    {
        if (_playerTarget == null)
            return;
        Vector3 direction = transform.position - _playerTarget.position;
        direction.y = 0f;

        float sqrMagnitude = direction.sqrMagnitude;
        if (sqrMagnitude <= 0.0001f)
            return;

        direction /= Mathf.Sqrt(sqrMagnitude);
        _knockbackVelocity += direction * knockbackForce;
    }

    private void ApplyColor(Color color)
    {
        if (targetRenderer == null)
            return;

        targetRenderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetColor(_colorPropertyId, color);
        targetRenderer.SetPropertyBlock(_propertyBlock);
    }

    public void Despawn()
    {
        if (!_isActive)
            return;

        _isActive = false;
        _knockbackVelocity = Vector3.zero;
        _hitFlashTimer = 0f;

        ApplyColor(defaultColor);

        _enemyRegistry.Unregister(this);
        _enemyPool.Return(this);
    }
}