using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public ObjectPool enemyPool;
    public List<Transform> spawnPoints = new List<Transform>();
    int pointCounter=0;
    public float spawnDelay;
    float timer;
    public int totalSpawn = 20;
    int spawnCount;
    bool spawning=true;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyPool == null) { enemyPool = FindObjectOfType<ObjectPool>(); }
        spawnPoints.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer && spawning)
        {
            GameObject spawn = enemyPool.GetObject("Enemy");
            spawn.transform.position = spawnPoints[pointCounter].position;
            pointCounter++;
            if (pointCounter == spawnPoints.Count) { pointCounter = 0; }
            spawnCount++;
            if (spawnCount == totalSpawn) { spawning = false; }
            timer = Time.time + spawnDelay;
        }
    }

    public void StopSpawning() { spawning = false; }
}
