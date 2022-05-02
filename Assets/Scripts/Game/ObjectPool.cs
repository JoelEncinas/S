using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sharedInstance;
    [SerializeField] private List<GameObject> pooledPlayerProjectiles;
    [SerializeField] private List<GameObject> pooledEnemyProjectiles;
    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private GameObject enemyProjectile;
    private int amountToPool = 20;

    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        pooledPlayerProjectiles = new List<GameObject>();
        pooledEnemyProjectiles = new List<GameObject>();
        AddToPool(pooledPlayerProjectiles, playerProjectile);
        AddToPool(pooledEnemyProjectiles, enemyProjectile);
    }

    public GameObject GetPooledObject(string poolname)
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (poolname == "pooledPlayerProjectiles")
            {
                if (!pooledPlayerProjectiles[i].activeInHierarchy)
                    return pooledPlayerProjectiles[i];
            }
            if (poolname == "pooledEnemyProjectiles")
            {
                if (!pooledEnemyProjectiles[i].activeInHierarchy)
                    return pooledEnemyProjectiles[i];
            }
        }

        return null;
    }

    public void AddToPool(List<GameObject> pool, GameObject objectToPool)
    {
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pool.Add(tmp);
        }
    }
}
