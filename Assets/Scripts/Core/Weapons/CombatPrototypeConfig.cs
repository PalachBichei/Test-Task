using UnityEngine;

[CreateAssetMenu(
    fileName = "CombatPrototypeConfig",
    menuName = "Game/Combat Prototype Config")]
public class CombatPrototypeConfig : ScriptableObject
{
    [Header("Enemy Spawn")]
    [Min(0.1f)] public float enemySpawnInterval = 0.25f;
    [Min(1)] public int maxEnemies = 100;
    [Min(0f)] public float spawnRadiusMin = 10f;
    [Min(0f)] public float spawnRadiusMax = 16f;

    [Header("Enemy Stats")]
    [Min(0.1f)] public float enemyMoveSpeed = 2.5f;
    [Min(1)] public int enemyHp = 5;

    [Header("Pool")]
    [Min(1)] public int prewarmEnemyCount = 120;
    
    [Header("Bullets")]
    [Min(0.05f)] public float bulletFireInterval = 0.25f;
    [Min(1)] public int bulletsPerShot = 1;
    [Min(0.1f)] public float bulletSpeed = 14f;
    [Min(1)] public int bulletDamage = 2;
    [Min(1)] public int prewarmBulletCount = 64;
    
    [Header("Grenades")]
    [Min(0.05f)] public float grenadeFireInterval = 1.5f;
    [Min(1)] public int grenadesPerShot = 1;
    [Min(0.1f)] public float grenadeFlightDuration = 0.6f;
    [Min(0f)] public float grenadeArcHeight = 2.5f;
    [Min(0.1f)] public float grenadeExplosionRadius = 2.5f;
    [Min(1)] public int grenadeDamage = 4;
    [Min(1)] public int prewarmGrenadeCount = 24;
}