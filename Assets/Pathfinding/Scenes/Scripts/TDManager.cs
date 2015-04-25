using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TDManager : MonoBehaviour 
{
	public int
		dirtCost,
		stoneCost,
		spikeTrapCost,
		arrowTrapCost,
		acidTrapCost;

	public GameObject spawn;
    public GameObject target;
	private Vector3 targetPos;
    private GameObject block;
    public GameObject ghostBlock;
    public GameObject enemy;
	public LayerMask buildLayer;
	public float spawnDelay = 5f;
	private bool place, canBuild;
	private LevelManager manager;
	private int souls;
	private bool subtracted;

	public AudioClip placeSound;
	public AudioClip cantplaceSound;

	//blocks
	public GameObject
		dirtPrefab,
		stonePrefab,
		spikePrefab,
		arrowPrefab,
		acidPrefab;

    private List<GameObject> blocks = new List<GameObject>();

    void Awake()
    {
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		spawn = GameObject.Find ("Spawn");
		target = GameObject.Find ("Player");

		StartCoroutine(SpawnEnemy());
    }
	
	void Update ()
    {
		souls = manager.souls;
		place = Input.GetMouseButtonDown (1);

		if(target)
			targetPos = target.transform.position;

		//if the player has souls, allow building
		if(canBuild && block != null)
		{
			ghostBlock.SetActive(true);
        	StartCoroutine(PlaceBlocks());
		}
		else
			ghostBlock.SetActive(false);

		if (manager.souls > 0)
			canBuild = true;
		else
			canBuild = false;
	}

	private void SubtractSoul()
	{
		if(block == dirtPrefab)
		{
			if(souls > dirtCost)
			{
				manager.SubtractSoul (dirtCost);
				subtracted = true;
			}
		}
		else if(block == stonePrefab)
		{
			if(souls > stoneCost)
			{
				manager.SubtractSoul (stoneCost);
				subtracted = true;
			}
		}
		else if(block == spikePrefab)
		{
			if(souls > spikeTrapCost)
			{
				manager.SubtractSoul (spikeTrapCost);
				subtracted = true;
			}
		}
		else if(block == acidPrefab)
		{
			if(souls > acidTrapCost)
			{
				manager.SubtractSoul (acidTrapCost);
				subtracted = true;
			}
		}
		else if(block == arrowPrefab)
		{
			if(souls > arrowTrapCost)
			{
				manager.SubtractSoul (arrowTrapCost);
				subtracted = true;
			}
		}
	}

    private RaycastHit CheckPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildLayer))
        {
            //Make sure to set blocks in a grid, by rounding position to an int
            Vector3 newPos = hit.point;
			newPos.Set(Mathf.RoundToInt(newPos.x) - 0.5F, Mathf.RoundToInt(newPos.y) + 0.5F, Mathf.RoundToInt(newPos.z) + 0.5F);
            ghostBlock.transform.position = newPos;

            //Set color of "show" tower based on the spot being available
			if (hit.transform.tag == "Ground" || hit.transform.tag == "Cube")
            {
				ghostBlock.GetComponent<Renderer>().sharedMaterial.color = Color.green;
            }
            else
            {
				ghostBlock.GetComponent<Renderer>().sharedMaterial.color = Color.red;
            }
        }
        else
        {
			ghostBlock.GetComponent<Renderer>().sharedMaterial.color = Color.red;
        }

        //Return all hit information which we use later
        return hit;
    }

    private IEnumerator PlaceBlocks()
    {
        RaycastHit hit = CheckPosition();
        bool canPlace = false;
        //Make sure that we did hit something
        if (hit.transform != null)
        {
			canPlace = (hit.transform.tag == "Ground" || hit.transform.tag == "Cube") ? true : false;
        }

        if (place && canPlace)
        {
			SubtractSoul();

			if(subtracted)
			{
				GameObject newBlock = Instantiate(block, new Vector3(Mathf.RoundToInt(hit.point.x) - 0.5F, Mathf.RoundToInt(hit.point.y) + 0.5F, Mathf.RoundToInt(hit.point.z) + 0.5F), Quaternion.identity) as GameObject;
	            blocks.Add(newBlock);
	            yield return new WaitForEndOfFrame();
	            Pathfinder.Instance.InsertInQueue(spawn.transform.position, targetPos, CheckRoute);
	            SoundManager.instance.PlaySingle (placeSound);
				subtracted = false;
			}
        }
    }

    private void CheckRoute(List<Vector3> list)
    {
        //If we get a list that is empty there is no path, and we blocked the road
        if (list.Count < 1 || list == null)
        {
			//delete blocking terrain
            if (blocks.Count > 0)
            {
				//get the last block in the list
                GameObject g = blocks[blocks.Count - 1];
				//remove it from the list
                blocks.RemoveAt(blocks.Count - 1);
				//destroy it
                Destroy(g);
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject e = Instantiate(enemy, spawn.transform.position, Quaternion.identity) as GameObject;
        e.GetComponent<TDEnemy>().spawn = spawn.transform;
		e.GetComponent<TDEnemy> ().target = target.transform;
        StartCoroutine(SpawnEnemy());
    }
	
	public void Dirt()
	{
		block = dirtPrefab;
	}
	public void Stone()
	{
		block = stonePrefab;
	}
	public void Spike()
	{
		block = spikePrefab;
	}
	public void Arrow()
	{
		block = arrowPrefab;
	}
	public void Acid()
	{
		block = acidPrefab;
	}
}
