using UnityEngine;
using VContainer;

public class AutoGrenadeThrower : AutoWeaponBase
{
    [SerializeField] private CombatPrototypeConfig config;
    [SerializeField] private GrenadePool grenadePool;

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
               && grenadePool != null
               && TargetSelector != null
               && _runtimeSettings != null;
    }

    protected override float GetInterval()
    {
        return _runtimeSettings.GrenadeFireInterval;
    }

    protected override void FireInternal()
    {
        Vector3 origin = GetOrigin();

        for (int i = 0; i < _runtimeSettings.GrenadesPerShot; i++)
        {
            EnemyController target = TargetSelector.SelectWeightedRandomTarget(origin);
            if (target == null)
                return;

            GrenadeProjectile grenade = grenadePool.Get();
            grenade.Spawn(
                origin,
                target.Position,
                config.grenadeFlightDuration,
                config.grenadeArcHeight,
                config.grenadeExplosionRadius,
                config.grenadeDamage);
        }
    }
}