using System.Collections.Generic;

public class EnemyRegistry
{
    private readonly List<EnemyController> _activeEnemies = new(128);

    public IReadOnlyList<EnemyController> ActiveEnemies => _activeEnemies;
    public int Count => _activeEnemies.Count;

    public void Register(EnemyController enemy)
    {
        if (enemy == null || _activeEnemies.Contains(enemy))
            return;

        _activeEnemies.Add(enemy);
    }

    public void Unregister(EnemyController enemy)
    {
        if (enemy == null)
            return;

        int index = _activeEnemies.IndexOf(enemy);
        if (index >= 0)
        {
            int lastIndex = _activeEnemies.Count - 1;
            _activeEnemies[index] = _activeEnemies[lastIndex];
            _activeEnemies.RemoveAt(lastIndex);
        }
    }
}