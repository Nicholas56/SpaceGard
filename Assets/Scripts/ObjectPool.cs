using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<Pool> pools = new List<Pool>();

    private void Awake()
    {
        for (int j = 0; j < pools.Count; j++)
        {
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject objectToPool = Instantiate(pools[j].objectPrefab,transform);
                pools[j].objectPool.Enqueue(objectToPool);
                objectToPool.SetActive(false);
            }
        }
    }

    public GameObject GetObject(string objectType)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].poolName == objectType)
            {
                if (pools[i].objectPool.Count > 0)
                {
                    GameObject objectToGet = pools[i].objectPool.Dequeue();
                    objectToGet.SetActive(true);
                    return objectToGet;
                }
                else
                {
                    GameObject objectToMake = Instantiate(pools[i].objectPrefab,transform);
                    return objectToMake;
                }
            }
        }
        return null;
    }

    public void ReturnObject(GameObject objectToReturn, string objectType)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].poolName == objectType)
            {
                pools[i].objectPool.Enqueue(objectToReturn);
                objectToReturn.SetActive(false);
            }
        }
    }
}
[System.Serializable]
public class Pool 
{
    public string poolName;
    public GameObject objectPrefab;
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    public int poolSize;
}

