using UnityEngine;
using System.Collections;

public class tdspawner : MonoBehaviour {

    public GameObject[] spawnArray;
    public GameObject spawn;
    public GameObject target;
	private Vector3 targetPos;
	private Vector3 spawnPos;

	//public GameObject enemy;
    public GameObject[] enemyPool1;
    public GameObject[] enemyPool2;

	public float range1 = -3.4f;
    public float range2 = .8f;
    //public float spawnX = -60f;
    //public float spawnY = 1.5f;
    public float startWait;
    public int enemyCount;
    public float spawnWait;
    public float waveWait;

    public float startWait2;
    public int enemyCount2;

	// Use this for initialization
	void Start () {
		//spawn = GameObject.Find ("Spawn");
		target = GameObject.Find ("Player");

		StartCoroutine(SpawnWaves1());
		StartCoroutine(SpawnWaves2());

		spawnPos = spawn.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(target)
			targetPos = target.transform.position;
	}

	IEnumerator SpawnWaves1()
    {

        yield return new WaitForSeconds (startWait);
        while (true)
        {	
        	for (int i = 0; i < enemyCount; i++)
            {
            	Vector3 spawnPosition = new Vector3 (spawnPos.x, spawnPos.y, Random.Range (range1, range2));

		        GameObject e1 = Instantiate(enemyPool1[Random.Range(0, enemyPool1.Length)], spawnPosition, Quaternion.identity) as GameObject;
				e1.GetComponentInChildren<TDEnemy> ().spawn = spawn.transform;
				e1.GetComponentInChildren<TDEnemy> ().target = target.transform;

				yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
		}
        enemyCount += 1;
    }

   	IEnumerator SpawnWaves2()
    {

        yield return new WaitForSeconds (startWait2);
        while (true)
        {	
			print ("YO");
        	for (int i = 0; i <= enemyCount2; i++)
            {
            	Vector3 spawnPosition = new Vector3 (spawnPos.x, spawnPos.y, Random.Range (range1, range2));

		        GameObject e2 = Instantiate(enemyPool2[Random.Range(0, enemyPool2.Length)], spawnPosition, Quaternion.identity) as GameObject;
				e2.GetComponentInChildren<TDEnemy> ().spawn = spawn.transform;
				e2.GetComponentInChildren<TDEnemy> ().target = target.transform;

				yield return new WaitForSeconds (spawnWait * 2);
            }
            yield return new WaitForSeconds (waveWait * 2);
		}
        enemyCount += 1;
    }
}
