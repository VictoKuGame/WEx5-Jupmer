using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] float time;
    [SerializeField] float repeatRate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", time, repeatRate);
    }

    void SpawnEnemies()
    {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
    }
}
