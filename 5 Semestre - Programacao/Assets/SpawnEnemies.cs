using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float intervalToSpawn;
    private float timer;
    private bool playerIsClose;

    private void Awake()
    {
        if (intervalToSpawn <= 0) intervalToSpawn = 5;
        timer = 0;
        playerIsClose = false;
    }

    private void Update()
    {
        if (timer <= intervalToSpawn) timer += 1 * Time.deltaTime;
        else
        {
            if(playerIsClose) SpawnEnemy();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        playerIsClose = true;
    }

    private void OnTriggerExit(Collider col)
    {
        playerIsClose = false;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, this.transform.position, Quaternion.identity);
        timer = 0;
    }
}
