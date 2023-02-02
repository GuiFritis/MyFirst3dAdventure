using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Padrao.Core.Singleton;

public abstract class PoolBase<T> : MonoBehaviour where T : Behaviour
{
    public int preWarmSize = 2;
    public bool finite = false;
    [ShowIf("maxSize")]
    public int maxPoolSize = 1000;
    [Tooltip("Object are created with name [poolName]_item")]
    public string poolName = "Obj";
    public T PFB_item;

    private List<T> _pool = new List<T>();
    private int _currentIndex;

    void OnValidate()
    {
        poolName = name;
    }

    protected abstract void Singleton();

    protected virtual void Awake()
    {
        Singleton();
        InitPool();
    }

    private void InitPool()
    {
        _pool = new List<T>();
        _currentIndex = 0;

        for (int i = 0; i < preWarmSize; i++)
        {
            CreatePoolItem();
        }
    }

    private void CreatePoolItem()
    {
        GameObject obj = Instantiate(PFB_item.gameObject, gameObject.transform);
        _pool.Add(obj.AddComponent<T>());
    }

    public T GetPoolItem()
    {
        T item = null;
        if(finite)
        {
            if(_currentIndex == _pool.Count)
            {
                if(maxPoolSize == _pool.Count)
                {
                    _currentIndex = 0;
                }
                else
                {
                    CreatePoolItem();
                }
            }
            item = _pool[_currentIndex];
            _currentIndex++;
        }
        else
        {
            item = _pool.Find(i => i.gameObject.activeInHierarchy);
            if(item == null)
            {
                CreatePoolItem();
                item = _pool[^1];
            }
        }
        return item;
    }

    public virtual void UseItem()
    {

    }
}
