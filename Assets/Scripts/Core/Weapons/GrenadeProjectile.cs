using UnityEngine;
using VContainer;

public class GrenadeProjectile : MonoBehaviour
{
    private EnemyRegistry _enemyRegistry;
    private GrenadePool _grenadePool;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _flightDuration;
    private float _arcHeight;
    private float _explosionRadius;
    private int _damage;

    private float _elapsed;
    private bool _isActive;

    [Inject]
    public void Construct(EnemyRegistry enemyRegistry, GrenadePool grenadePool)
    {
        _enemyRegistry = enemyRegistry;
        _grenadePool = grenadePool;
    }

    public void Spawn(
        Vector3 startPosition,
        Vector3 targetPosition,
        float flightDuration,
        float arcHeight,
        float explosionRadius,
        int damage)
    {
        _startPosition = startPosition;
        _targetPosition = targetPosition;
        _flightDuration = Mathf.Max(0.01f, flightDuration);
        _arcHeight = arcHeight;
        _explosionRadius = explosionRadius;
        _damage = damage;

        _elapsed = 0f;
        _isActive = true;

        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!_isActive)
            return;

        _elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(_elapsed / _flightDuration);

        Vector3 flatPosition = Vector3.Lerp(_startPosition, _targetPosition, t);
        float arc = Mathf.Sin(t * Mathf.PI) * _arcHeight;
        transform.position = flatPosition + Vector3.up * arc;

        if (t >= 1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        var enemies = _enemyRegistry.ActiveEnemies;
        float radiusSqr = _explosionRadius * _explosionRadius;

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyController enemy = enemies[i];
            if (enemy == null || !enemy.IsAlive)
                continue;

            Vector3 diff = enemy.Position - _targetPosition;
            diff.y = 0f;

            if (diff.sqrMagnitude <= radiusSqr)
            {
                enemy.ApplyDamage(_damage);
            }
        }

        Despawn();
    }

    private void Despawn()
    {
        if (!_isActive)
            return;

        _isActive = false;
        _grenadePool.Return(this);
    }
}