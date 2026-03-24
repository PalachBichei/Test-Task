using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private CombatPrototypeConfig combatPrototypeConfig;

    [SerializeField] private PlayerInputReader playerInputReader;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private GrenadePool grenadePool;
    [SerializeField] private AutoShooter autoShooter;
    [SerializeField] private AutoGrenadeThrower autoGrenadeThrower;
    [SerializeField] private DebugPanelView debugPanelView;

    protected override void Configure(IContainerBuilder builder)
    {
        if (playerInputReader != null)
            builder.RegisterComponent(playerInputReader);

        if (enemySpawner != null)
            builder.RegisterComponent(enemySpawner);

        if (enemyPool != null)
            builder.RegisterComponent(enemyPool);

        if (bulletPool != null)
            builder.RegisterComponent(bulletPool);

        if (grenadePool != null)
            builder.RegisterComponent(grenadePool);

        if (autoShooter != null)
            builder.RegisterComponent(autoShooter);

        if (autoGrenadeThrower != null)
            builder.RegisterComponent(autoGrenadeThrower);

        if (debugPanelView != null)
            builder.RegisterComponent(debugPanelView);

        builder.Register<EnemyRegistry>(Lifetime.Singleton);
        builder.Register<TargetSelector>(Lifetime.Singleton);

        RuntimeCombatSettings runtimeSettings =
            RuntimeCombatSettingsProvider.CreateFromConfig(combatPrototypeConfig);

        builder.RegisterInstance(runtimeSettings);
    }
}