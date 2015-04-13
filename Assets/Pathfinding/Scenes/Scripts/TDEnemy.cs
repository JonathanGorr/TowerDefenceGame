using UnityEngine;
using System.Collections;

public class TDEnemy : Pathfinding
{
	private TDManager tdManager;
	public Vector3 spawn;
	public Vector3 target;

	[HideInInspector]
	public GameObject closest;

    private bool pathMover = true;
    private bool newPath = true;

	public float speed = 1f;

	void Awake()
	{
		tdManager = GameObject.Find ("LevelManager").GetComponent<TDManager>();
	}

	void Update ()
    {
		spawn = tdManager.spawn.transform.position;//GameObject.Find ("Start").transform.position;
		target = tdManager.target.transform.position;//GameObject.Find ("Player").transform.position;

        if (transform.position.x < 10.2F)
        {
            if (newPath)
            {
                StartCoroutine(PathTimer());
            }

            Movement();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
	}

    IEnumerator PathTimer()
    {
        newPath = false;
        FindPath(transform.position, target);
        yield return new WaitForSeconds(0.5F);
        newPath = true;
    }

    private void Movement()
    {
        if (Path.Count > 0)
        {
            if (pathMover)
            {
                //StartCoroutine(PathRemoval(4F + 2F));
            }

            if (Vector3.Distance(transform.position, new Vector3(Path[0].x, transform.position.y, Path[0].z)) < 0.2F)
            {
                Path.RemoveAt(0);
            }

            if (Path.Count > 0)
            {             
                Vector3 direction = (new Vector3(Path[0].x, transform.position.y, Path[0].z) - transform.position).normalized;
                if (direction == Vector3.zero)
                {
                   // direction = (end - transform.position).normalized;
                }
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * speed);
            }
        }
    }

	public void AttackClosest()
	{
		target = FindClosest().transform.position;
	}

	public GameObject FindClosest() {

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		float distance = Mathf.Infinity;

		//foreach gameobject in gameobjects, find closest,
		//then return that closest gameobject

		foreach (GameObject go in gos) {

			Vector3 diff = go.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

    IEnumerator PathRemoval(float speed)
    {
        pathMover = false;
        yield return new WaitForSeconds((1 * Pathfinder.Instance.Tilesize) / speed);
        if (Path.Count > 0)
        {
            Path.RemoveAt(0);
        }
        pathMover= true;
    }
}
