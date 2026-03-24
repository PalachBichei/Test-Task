using UnityEngine;
using VContainer;

public class BulletProjectile : MonoBehaviour
{
    private BulletPool _bulletPool;

    private EnemyController _target;
    private float _speed;
    private int _damage;
    private bool _isActive;

    [Inject]
    public void Construct(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Spawn(Vector3 startPosition, EnemyController target, float speed, int damage)
    {
        transform.position = startPosition;
        _target = target;
        _speed = speed;
        _damage = damage;
        _isActive = true;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (_target == null || !_target.IsAlive)
        {
            Despawn();
            return;
        }

        Vector3 targetPosition = _target.Position;
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;
        if (distance <= 0.15f)
        {
            _target.ApplyDamage(_damage);
            Despawn();
            return;
        }

        direction /= distance;
        transform.position += direction * (_speed * Time.deltaTime);
        transform.forward = direction;
    }

    private void Despawn()
    {
        if (!_isActive)
            return;

        _isActive = false;
        _target = null;
        _bulletPool.Return(this);
    }
}