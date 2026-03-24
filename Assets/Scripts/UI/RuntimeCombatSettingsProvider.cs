using UnityEngine;

public static class RuntimeCombatSettingsProvider
{
    public static RuntimeCombatSettings CreateFromConfig(CombatPrototypeConfig config)
    {
        if (config == null)
        {
            return new RuntimeCombatSettings
            {
                EnemySpawnInterval = 0.25f,
                MaxEnemies = 100,
                BulletFireInterval = 0.25f,
                BulletsPerShot = 1,
                GrenadeFireInterval = 1.5f,
                GrenadesPerShot = 1
            };
        }

        return new RuntimeCombatSettings
        {
            EnemySpawnInterval = config.enemySpawnInterval,
            MaxEnemies = config.maxEnemies,
            BulletFireInterval = config.bulletFireInterval,
            BulletsPerShot = config.bulletsPerShot,
            GrenadeFireInterval = config.grenadeFireInterval,
            GrenadesPerShot = config.grenadesPerShot
        };
    }
}