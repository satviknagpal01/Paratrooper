using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private Transform parentTransform;
    private List<T> pool;

    public ObjectPool(T prefab, Transform parentTransform, int initialSize)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        this.pool = new List<T>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    private T CreateNewObject()
    {
        T newObj = Object.Instantiate(prefab, parentTransform);
        newObj.gameObject.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public T GetObject()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        // If no inactive object is found, create a new one.
        T newObj = CreateNewObject();
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void ReturnAllObjects()
    {
        foreach (T obj in pool)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
