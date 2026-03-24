using UnityEngine;

public class GrenadePool : ComponentPool<GrenadeProjectile>
{
    [SerializeField] private CombatPrototypeConfig config;

    protected override void Awake()
    {
        if (config != null)
            prewarmCount = config.prewarmGrenadeCount;

        base.Awake();
    }
}