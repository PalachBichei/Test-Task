using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform poolRoot;
    [SerializeField] private CombatPrototypeConfig config;

    private readonly Queue<EnemyController> _pool = new();
    private IObjectResolver _resolver;

    [Inject]
    public void Construct(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    private void Awake()
    {
        if (poolRoot == null)
            poolRoot = transform;

        Prewarm();
    }

    private void Prewarm()
    {
        int count = config != null ? config.prewarmEnemyCount : 100;

        for (int i = 0; i < count; i++)
        {
            EnemyController enemy = CreateInstance();
            Return(enemy);
        }
    }

    private EnemyController CreateInstance()
    {
        EnemyController enemy = Instantiate(enemyPrefab, poolRoot);
        _resolver.Inject(enemy);
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    public EnemyController Get()
    {
        if (_pool.Count > 0)
            return _pool.Dequeue();

        return CreateInstance();
    }

    public void Return(EnemyController enemy)
    {
        if (enemy == null)
            return;

        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(poolRoot, false);
        _pool.Enqueue(enemy);
    }
}