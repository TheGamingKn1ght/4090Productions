using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private int maxEnemyCount = 60;
    private static int currentEnemyCount;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    private void Start()
    {
        currentEnemyCount = 0;
    }
    private void Update()
    {
        while(currentEnemyCount <= maxEnemyCount)
        {
            foreach (var spawnpoint in spawnPoints)
            {
                Vector3 spawn = new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z);
                Instantiate(enemyPrefab, spawn, Quaternion.identity);
                enemyList.Add(enemyPrefab);
                currentEnemyCount++;
            }
        }
        /*
        foreach(var enemy in enemyList)
        {
            if (enemy.GetComponent<Enemy>().isDead == true)
            {
                Destroy(enemy);
                currentEnemyCount--;
            }
        }
        */
    }

}
