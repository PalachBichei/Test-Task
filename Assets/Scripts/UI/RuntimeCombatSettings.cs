public class RuntimeCombatSettings
{
    public float EnemySpawnInterval { get; set; }
    public int MaxEnemies { get; set; }

    public float BulletFireInterval { get; set; }
    public int BulletsPerShot { get; set; }

    public float GrenadeFireInterval { get; set; }
    public int GrenadesPerShot { get; set; }

    public RuntimeCombatSettings Clone()
    {
        return new RuntimeCombatSettings
        {
            EnemySpawnInterval = EnemySpawnInterval,
            MaxEnemies = MaxEnemies,
            BulletFireInterval = BulletFireInterval,
            BulletsPerShot = BulletsPerShot,
            GrenadeFireInterval = GrenadeFireInterval,
            GrenadesPerShot = GrenadesPerShot
        };
    }
}