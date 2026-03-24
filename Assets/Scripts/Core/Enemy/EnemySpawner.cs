using UnityEngine;
using VContainer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private CombatPrototypeConfig config;
    [SerializeField] private EnemyPool enemyPool;

    private EnemyRegistry _enemyRegistry;
    private RuntimeCombatSettings _runtimeSettings;
    private float _spawnTimer;

    [Inject]
    public void Construct(
        EnemyRegistry enemyRegistry,
        RuntimeCombatSettings runtimeSettings)
    {
        _enemyRegistry = enemyRegistry;
        _runtimeSettings = runtimeSettings;
    }

    private void Update()
    {
        if (playerTarget == null || config == null || enemyPool == null || _enemyRegistry == null || _runtimeSettings == null)
            return;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer > 0f)
            return;

        _spawnTimer = _runtimeSettings.EnemySpawnInterval;

        if (_enemyRegistry.Count >= _runtimeSettings.MaxEnemies)
            return;

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 random2D = Random.insideUnitCircle.normalized;
        if (random2D.sqrMagnitude < 0.0001f)
            random2D = Vector2.right;

        float distance = Random.Range(config.spawnRadiusMin, config.spawnRadiusMax);
        Vector3 spawnPosition = playerTarget.position + new Vector3(random2D.x, 0f, random2D.y) * distance;

        EnemyController enemy = enemyPool.Get();
        enemy.Spawn(
            playerTarget,
            spawnPosition,
            config.enemyMoveSpeed,
            config.enemyHp);
    }
}