using UnityEngine;
using System.Collections;

public class TDEnemy : Pathfinding
{
	//transforms
	[HideInInspector]
    public Transform spawn, target;

	//values
	public float speed = 1f;

	[HideInInspector]
    public bool pathMover = true, newPath = true;

	public void StartTimer()
	{
		StartCoroutine ("PathTimer");
	}

    IEnumerator PathTimer()
    {
        newPath = false;
        FindPath(transform.position, target.position);
        yield return new WaitForSeconds(0.5F);
        newPath = true;
    }

    public void Movement()
    {
        if (Path.Count > 0)
        {
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
