using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject enemy;
    private GameObject currentEnemy;

    [SerializeField]
    private int maxEnemies;
    public int currentEnemies;
    //Counter
    public float counter;

    //Time Between Spawns
    public float delay;

    //Amount
    public float enemeySpawns;

    //FailSafe to ensure exacly as wanted
    public bool spawned;

    public int StartGame = 0;
    private void Update()
    {
        if (StartGame >= 3)
        {
            SpawnEnemies(enemeySpawns);
        }
    }

    private void SpawnEnemies(float amount)
    {
        counter = counter + Time.deltaTime;
        if (currentEnemies >= maxEnemies)
        {
            return;
        }

        if (counter > delay && spawned)
        {
            for (int i = 0; i < amount; i++)
            {
                for (int j = 0; j < spawnPoints.Length; j++)
                {
                    currentEnemy = PhotonNetwork.Instantiate(enemy.name, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);
                    currentEnemy.GetComponent<AI>().rightDes = j;
                    currentEnemies++;
                    if (j > 2)
                        j = 0;

                    print($"{i}, {j}");
                }
            }
            spawned = false;
            counter = 0;
        }
        else
            spawned = true;
    }
}
