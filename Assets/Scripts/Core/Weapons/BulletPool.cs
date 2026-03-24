using UnityEngine;

public class BulletPool : ComponentPool<BulletProjectile>
{
    [SerializeField] private CombatPrototypeConfig config;

    protected override void Awake()
    {
        if (config != null)
            prewarmCount = config.prewarmBulletCount;

        base.Awake();
    }
}