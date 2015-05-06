using UnityEngine;
using System.Collections;

public class tdspawner : MonoBehaviour {

	//arrays and gameobjects
    public GameObject[] spawnArray;
    public GameObject spawn, target;

	//components
	private RotateSun cycle;

	//vectors
	private Vector3 targetPos, spawnPos;

	//public GameObject enemy;
    public GameObject[] enemyPool1, enemyPool2;

	//floats
	public float 
		range1 = -3.4f,
		range2 = 0.8f,
		startWait,
		spawnWait,
		waveWait,
		startWait2;

	public int enemyCount, enemyCount2;
	
	void Awake () 
	{
		cycle = GameObject.Find("DayNightCycle").GetComponent<RotateSun>();
		target = GameObject.Find ("Player");

		StartCoroutine(SpawnWaves1(.5f));
		StartCoroutine(SpawnWaves2(.5f));

		spawnPos = spawn.transform.position;
	}

	void Update ()
	{
		if(target)
			targetPos = target.transform.position;
	}

	IEnumerator SpawnWaves1(float interval)
    {
        yield return new WaitForSeconds (startWait);
        
		while(true)
		{
			if(cycle.CurrentState == RotateSun.States.Dusk ||
			   cycle.CurrentState == RotateSun.States.Night)
			{
				print("Spawning enemies");

	        	for (int i = 0; i < enemyCount; i++)
	            {
	            	Vector3 spawnPosition = new Vector3 (spawnPos.x, spawnPos.y, Random.Range (range1, range2));

			        GameObject e1 = Instantiate(enemyPool1[Random.Range(0, enemyPool1.Length)], spawnPosition, Quaternion.identity) as GameObject;
					e1.GetComponentInChildren<TDEnemy> ().spawn = spawn.transform;
					e1.GetComponentInChildren<TDEnemy> ().target = target.transform;

					yield return new WaitForSeconds (spawnWait);
	            }
	            yield return new WaitForSeconds (waveWait);
	    		enemyCount += 1;
			}
			yield return new WaitForSeconds(interval);
		}
    }

   	IEnumerator SpawnWaves2(float interval)
    {
        yield return new WaitForSeconds (startWait2);

		while(true)
		{
			if(cycle.CurrentState == RotateSun.States.Dusk ||
			   cycle.CurrentState == RotateSun.States.Night)
			{
				print("Spawning tougher enemies");

	        	for (int i = 0; i <= enemyCount2; i++)
	            {
	            	Vector3 spawnPosition = new Vector3 (spawnPos.x, spawnPos.y, Random.Range (range1, range2));

			        GameObject e2 = Instantiate(enemyPool2[Random.Range(0, enemyPool2.Length)], spawnPosition, Quaternion.identity) as GameObject;
					e2.GetComponentInChildren<TDEnemy> ().spawn = spawn.transform;
					e2.GetComponentInChildren<TDEnemy> ().target = target.transform;

					yield return new WaitForSeconds (spawnWait * 2);
	            }
	            yield return new WaitForSeconds (waveWait * 2);
	    		enemyCount += 1;
			}
			yield return new WaitForSeconds(interval);
		}
    }
}
