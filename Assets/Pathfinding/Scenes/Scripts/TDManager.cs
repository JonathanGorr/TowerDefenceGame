using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TDManager : MonoBehaviour 
{
    public GameObject spawn;
    public GameObject target;
	private Vector3 targetPos;
    public GameObject tower;
    public GameObject ghostTower;
    public GameObject enemy;
	public LayerMask buildLayer;
	public float spawnDelay = 5f;
	private bool place;

    private List<GameObject> towers = new List<GameObject>();

    void Awake()
    {
		spawn = GameObject.Find ("Spawn");
		target = GameObject.Find ("Player");

		StartCoroutine(SpawnEnemy());
    }
	
	void Update ()
    {
		place = Input.GetMouseButtonDown (1);
		targetPos = target.transform.position;
        StartCoroutine(PlaceTowers());
	}

    private RaycastHit CheckPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildLayer))
        {
            //Make sure to set towers in a grid, by rounding position to an int
            Vector3 newPos = hit.point;
            newPos.Set(Mathf.RoundToInt(newPos.x) - 0.5F, 0.4F, Mathf.RoundToInt(newPos.z) + 0.5F);
            ghostTower.transform.position = newPos;

            //Set color of "show" tower based on the spot being available
            if (hit.transform.tag == "Ground")
            {
                ghostTower.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                ghostTower.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else
        {
            ghostTower.GetComponent<Renderer>().material.color = Color.red;
        }

        //Return all hit information which we use later
        return hit;
    }

    private IEnumerator PlaceTowers()
    {
        RaycastHit hit = CheckPosition();
        bool canPlace = false;
        //Make sure that we did hit something
        if (hit.transform != null)
        {
            canPlace = (hit.transform.tag == "Ground") ? true : false;
        }

        if (place && canPlace)
        {
            GameObject newTower = Instantiate(tower, new Vector3(Mathf.RoundToInt(hit.point.x) - 0.5F, 0.3F, Mathf.RoundToInt(hit.point.z) + 0.5F), Quaternion.identity) as GameObject;
            towers.Add(newTower);
            yield return new WaitForEndOfFrame();
            Pathfinder.Instance.InsertInQueue(spawn.transform.position, targetPos, CheckRoute);
        }      
    }

    private void CheckRoute(List<Vector3> list)
    {
        //If we get a list that is empty there is no path, and we blocked the road
        if (list.Count < 1 || list == null)
        {
			/*
			GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

			foreach(GameObject enemy in Enemies)
			{
				print("Attack Blocks");
				enemy.GetComponent<TDEnemy>().AttackClosest();
			}
			*/

			//delete blocking terrain
            if (towers.Count > 0)
            {
                GameObject g = towers[towers.Count - 1];
                towers.RemoveAt(towers.Count - 1);
                Destroy(g);
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject e = Instantiate(enemy, spawn.transform.position, Quaternion.identity) as GameObject;
        e.GetComponent<TDEnemy>().spawn = spawn.transform.position;
		e.GetComponent<TDEnemy> ().target = target.transform;
        StartCoroutine(SpawnEnemy());
    }
}
