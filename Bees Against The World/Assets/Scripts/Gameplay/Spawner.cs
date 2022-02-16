using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
 {
    #region PUBLIC_VARS
    public static Spawner Instance;
    [Header("Enemy")]
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public List<Transform> enemies = new List<Transform>();
    [Header("Bee")]
    public List<Transform> beeSpawnPoints = new List<Transform>();
    public List<Transform> bees = new List<Transform>();
    [Header("Resource")]
    public List<Transform> resourceSpawnPoints = new List<Transform>();
    public List<Transform> resources = new List<Transform>();
    #endregion

    #region PRIVATE_VARS
    private List<Transform> enemiesSpawned = new List<Transform>();
    private List<Transform> beesSpawned = new List<Transform>();
    #endregion

    #region UNITY_CALLBACKS
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    public void ResetForGatheringPhase()
    {
        for(int i=0;i<enemiesSpawned.Count;i++)
        {
            if(enemiesSpawned[i] != null)
            {
                Destroy(enemiesSpawned[i].gameObject);
            }
        }
        for (int i = 0; i < beesSpawned.Count; i++)
        {
            if (beesSpawned[i] != null)
            {
                Destroy(beesSpawned[i].gameObject);
            }
        }
        enemiesSpawned.Clear();
        beesSpawned.Clear();
    }

    public void Spawn(Transform prefeb)
    {
        if(prefeb == bees[0])
        {
            SpawnBees(1);
        }
        else
        {
            SpawnEnemy(1);
        }

        if(resources.Count != 0 && prefeb == resources[0])
        {
            SpawnResource(1);
        }
    }
    public void SpawnEnemy(int cnt = 3)
    {
        if(cnt > enemySpawnPoints.Count)
        {
            cnt = enemySpawnPoints.Count;
        }

        for(int i=0;i<cnt;i++)
        {
            enemiesSpawned.Add(Instantiate(enemies[Random.Range(0, enemies.Count)], enemySpawnPoints[i]));
        }
    }
    public void SpawnBees(int cnt = 3)
    {
        if(cnt > beeSpawnPoints.Count)
        {
            cnt = beeSpawnPoints.Count;
        }

        for(int i=0;i<cnt;i++)
        {
            beesSpawned.Add(Instantiate(bees[Random.Range(0, bees.Count)], beeSpawnPoints[i].position + new Vector3(Random.Range(-1, 1), 1, Random.Range(-2, 2)), Quaternion.identity, beeSpawnPoints[i]));
        }
    }
    public void SpawnResource(int cnt = 10)
    {
        if (cnt > resourceSpawnPoints.Count)
        {
            cnt = resourceSpawnPoints.Count;
        }

        for (int i = 0; i < cnt; i++)
        {
            Instantiate(resources[Random.Range(0, resources.Count)], resourceSpawnPoints[i].position + new Vector3(Random.Range(-3,3),1,Random.Range(-2,2)), Quaternion.identity, resourceSpawnPoints[i]);
        }
    }
    #endregion

    #region PRIVATE_FUNCTIONS
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    #endregion
}