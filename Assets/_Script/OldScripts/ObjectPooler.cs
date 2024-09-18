using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public GameObject roadPrefab;
    public int poolSize = 10;
    private Queue<GameObject> roadPool;

    private void Awake()
    {
        Instance = this;

        // Initialize the pool
        roadPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(false);
            roadPool.Enqueue(road);
        }
    }

    public GameObject GetPooledObject()
    {
        if (roadPool.Count > 0)
        {
            GameObject road = roadPool.Dequeue();
            road.SetActive(true);
            return road;
        }
        else
        {
            // Optional: Instantiate more objects if the pool is empty
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(true);
            return road;
        }
    }

    public void ReturnObjectToPool(GameObject road)
    {
        road.SetActive(false);
        roadPool.Enqueue(road);
    }
}
