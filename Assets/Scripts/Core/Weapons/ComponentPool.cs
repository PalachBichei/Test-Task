using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public abstract class ComponentPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;
    [SerializeField] protected Transform poolRoot;
    [SerializeField] protected int prewarmCount = 32;

    protected readonly Queue<T> Pool = new();
    protected IObjectResolver Resolver;

    [Inject]
    public void Construct(IObjectResolver resolver)
    {
        Resolver = resolver;
    }

    protected virtual void Awake()
    {
        if (poolRoot == null)
            poolRoot = transform;

        Prewarm();
    }

    protected virtual void Prewarm()
    {
        for (int i = 0; i < prewarmCount; i++)
        {
            T instance = CreateInstance();
            Return(instance);
        }
    }

    protected virtual T CreateInstance()
    {
        T instance = Instantiate(prefab, poolRoot);
        Resolver.Inject(instance);
        instance.gameObject.SetActive(false);
        return instance;
    }

    public virtual T Get()
    {
        return Pool.Count > 0 ? Pool.Dequeue() : CreateInstance();
    }

    public virtual void Return(T instance)
    {
        if (instance == null)
            return;

        instance.gameObject.SetActive(false);
        instance.transform.SetParent(poolRoot, false);
        Pool.Enqueue(instance);
    }
}