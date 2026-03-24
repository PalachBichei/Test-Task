using UnityEngine;

public class TargetSelector
{
    private readonly EnemyRegistry _enemyRegistry;

    public TargetSelector(EnemyRegistry enemyRegistry)
    {
        _enemyRegistry = enemyRegistry;
    }

    public EnemyController SelectWeightedRandomTarget(Vector3 origin)
    {
        var enemies = _enemyRegistry.ActiveEnemies;
        int count = enemies.Count;

        if (count == 0)
            return null;

        float totalWeight = 0f;

        for (int i = 0; i < count; i++)
        {
            EnemyController enemy = enemies[i];
            if (enemy == null || !enemy.IsAlive)
                continue;

            Vector3 diff = enemy.Position - origin;
            diff.y = 0f;

            float distanceSqr = diff.sqrMagnitude;
            float weight = 1f / (distanceSqr + 0.25f);
            totalWeight += weight;
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.value * totalWeight;
        float accumulated = 0f;

        for (int i = 0; i < count; i++)
        {
            EnemyController enemy = enemies[i];
            if (enemy == null || !enemy.IsAlive)
                continue;

            Vector3 diff = enemy.Position - origin;
            diff.y = 0f;

            float distanceSqr = diff.sqrMagnitude;
            float weight = 1f / (distanceSqr + 0.25f);

            accumulated += weight;
            if (randomValue <= accumulated)
                return enemy;
        }

        return enemies[count - 1];
    }
}