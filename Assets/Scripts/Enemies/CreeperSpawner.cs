﻿using UnityEngine;
using System.Collections;

public class CreeperSpawner : MonoBehaviour {

	public GameObject[] enemyPool;
    public float range1 = -5.5f;
    public float range2 = -1.5f;
    public float spawnX = -60f;
    public float spawnY = 1.5f;
    public int enemyCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start ()
    {
		if(GetComponent<LevelManager>().inMenu == false)
        	StartCoroutine (SpawnWaves ());
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (spawnX, 1.5f, Random.Range (range1, range2));
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (enemyPool[Random.Range(0, enemyPool.Length)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
            //spawnWait -= .2f;
            //waveWait -= .2f;
            enemyCount += 1;
        }
    }
}
