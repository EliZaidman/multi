using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    
    [SerializeField]
    private GameObject enemy;
    private GameObject currentEnemy;
    //Counter
    public float counter;

    //Time Between Spawns
    public float delay;

    //Amount
    public int enemeySpawns;

    //FailSafe to ensure exacly as wanted
    public bool spawned;

    private void Update()
    {
        SpawnEnemies(enemeySpawns);
    }

    private void SpawnEnemies(int amount)
    {
        counter = counter + Time.deltaTime;
        if (counter > delay && spawned)
        {
            for (int i = 0; i < amount; i++)
            {
                for (int j = 0; j < spawnPoints.Length; j++)
                {
                    Instantiate(enemy, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);

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
