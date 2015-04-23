using UnityEngine;
using System.Collections;

public class TDEnemy : Pathfinding
{
	//vectors
	public Vector3 spawn;

	//transforms
	public Transform target;

	//components
	private StateMachine stateMachine;

	[HideInInspector]
	public GameObject closest;

	//bools
	[HideInInspector]
	public bool pathMover = true, newPath = true;

	private void Awake()
	{
		stateMachine = GetComponent<StateMachine> ();
	}

	public void StartTimer()
	{
		StartCoroutine("PathTimer");
	}

    IEnumerator PathTimer()
    {
        newPath = false;
        FindPath(transform.position, target.transform.position);
        yield return new WaitForSeconds(0.5F);
        newPath = true;
    }

    public void Movement(float speed)
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
				if(!stateMachine.attacking)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * speed);
				}
            }
        }
    }

	public virtual GameObject FindClosest() {

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

    public IEnumerator PathRemoval(float speed)
    {
        pathMover = false;
       
		yield return new WaitForSeconds((1 * Pathfinder.Instance.Tilesize) / speed);
       
		if (Path.Count > 0)
        {
            Path.RemoveAt(0);
        }
        pathMover = true;
    }
}