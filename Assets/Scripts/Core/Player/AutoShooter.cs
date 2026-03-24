using UnityEngine;
using VContainer;

public class AutoShooter : AutoWeaponBase
{
    [SerializeField] private CombatPrototypeConfig config;
    [SerializeField] private BulletPool bulletPool;

    private RuntimeCombatSettings _runtimeSettings;

    [Inject]
    public void Construct(TargetSelector targetSelector, RuntimeCombatSettings runtimeSettings)
    {
        SetTargetSelector(targetSelector);
        _runtimeSettings = runtimeSettings;
    }

    protected override bool CanRun()
    {
        return config != null
               && bulletPool != null
               && TargetSelector != null
               && _runtimeSettings != null;
    }

    protected override float GetInterval()
    {
        return _runtimeSettings.BulletFireInterval;
    }

    protected override void FireInternal()
    {
        Vector3 origin = GetOrigin();

        for (int i = 0; i < _runtimeSettings.BulletsPerShot; i++)
        {
            EnemyController target = TargetSelector.SelectWeightedRandomTarget(origin);
            if (target == null)
                return;

            BulletProjectile bullet = bulletPool.Get();
            bullet.Spawn(
                origin,
                target,
                config.bulletSpeed,
                config.bulletDamage);
        }
    }
}