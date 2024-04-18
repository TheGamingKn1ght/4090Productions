using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private int maxEnemyCount = 60;
    public int currentEnemyCount;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] public List<GameObject> enemyList = new List<GameObject>();

    [SerializeField] private GameObject FadePlayer;
    [SerializeField] private float FadeWaitTime = 1.2f;
    private float currentFadeWaitTime = 0f;

    public static bool cureDiscovered;
    private void OnEnable()
    {
        Enemy.OnEnemyDeath += KillEnemy;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= KillEnemy;
    }

    private void Start()
    {
        currentEnemyCount = 0;
    }
    private void Update()
    {
        if (cureDiscovered == false)
        {
            while (currentEnemyCount <= maxEnemyCount)
            {
                foreach (var spawnpoint in spawnPoints)
                {
                    Debug.Log("Spawning Enemy");
                    Vector3 spawn = new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z);
                    Instantiate(enemyPrefab, spawn, Quaternion.identity);
                    enemyList.Add(enemyPrefab);
                    currentEnemyCount++;
                    StartCoroutine(SpawnAnotherEnemy());
                }
            }
        }
        if (currentEnemyCount == 0)
        {
            EndGame();
        }
    }

    private void KillEnemy()
    {
        currentEnemyCount--;
        enemyList.RemoveAt(0);
        
    }
    private void EndGame()
    {
        currentFadeWaitTime += Time.deltaTime;
        if (currentFadeWaitTime >= FadeWaitTime / 2)
        {
            FadePlayer.SetActive(true);
        }
        if (currentFadeWaitTime >= FadeWaitTime)
        {
            currentFadeWaitTime = 0f;
            Time.timeScale = 1f;
            FadePlayer.SetActive(false);
            SceneManager.LoadSceneAsync(0);
        }
    }

    IEnumerator SpawnAnotherEnemy()
    {
        yield return new WaitForSecondsRealtime(1);
    }

}
