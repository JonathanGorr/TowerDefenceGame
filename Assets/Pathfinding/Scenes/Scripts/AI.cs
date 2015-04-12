using UnityEngine;
using System.Collections;

public class AI : Pathfinding {

    public Transform target;
    private CharacterController controller;
    private bool newPath = true;
    private bool moving = false;
    private GameObject[] AIList;
	private float maxY;
	private float snapValue = 0.5f;
	private float seekDistance = 25f;
	private Vector2 distance;
	public float speed = 5f;

	[HideInInspector]
	public Vector3 direction;

	void Awake ()
    {
		controller = GetComponent<CharacterController> ();
        AIList = GameObject.FindGameObjectsWithTag("Enemy");
		//target = GameObject.Find ("Heart").transform;
		target = GameObject.Find ("Player").transform;
		maxY = transform.position.y;
	}
	
	void Update ()
    {
		distance = new Vector2((target.transform.position.x - transform.position.x), (target.transform.position.z - transform.position.z));

		//if the target is too far away, create a new path and move to it, setting move bool to true
        if (Vector3.Distance(target.position, transform.position) < seekDistance && !moving)
        {
            if (newPath)
            {
                StartCoroutine(NewPath());
            }
            moving = true;
        }

		//else if close enough, stop
        else if (Vector3.Distance(target.position, transform.position) < 2F)
        {
            //Stop!
			//Attack!
        }

		//else if its still too far away, restructure path and keep moving
        else if (Vector3.Distance(target.position, transform.position) < 35F && moving)
        {
            if (Path.Count > 0)
            {
                if (Vector3.Distance(target.position, Path[Path.Count - 1]) > 5F)
                {
                    StartCoroutine(NewPath());
                }
            }
            else
            {
                if (newPath)
                {
                    StartCoroutine(NewPath());
                }
            }
            //Move the ai towards the target
            MoveMethod();
        }
		//else not moving
        else
        {
            moving = false;
        }
	}

    IEnumerator NewPath()
    {
        newPath = false;
        FindPath(transform.position, target.position);
        yield return new WaitForSeconds(1F);
        newPath = true;
    }
	
    private void MoveMethod()
    {
        if (Path.Count > 0)
        {
            direction = (Path[0] - transform.position).normalized;

            foreach (GameObject g in AIList)
            {
                if(Vector3.Distance(g.transform.position, transform.position) < 1F)
                {
                    Vector3 dir = (transform.position - g.transform.position).normalized;
                    dir.Set(dir.x, 0, dir.z);
                    direction += 0.2F * dir;
                }
            }

            direction.Normalize();

			//controller.Move(direction * Time.deltaTime * speed);
			transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * speed);

            if (transform.position.x < Path[0].x + 0.4F && transform.position.x > Path[0].x - 0.4F && transform.position.z > Path[0].z - 0.4F && transform.position.z < Path[0].z + 0.4F)
            {
                Path.RemoveAt(0);
            }

            RaycastHit[] hit = Physics.RaycastAll(transform.position + (Vector3.up * 20F), Vector3.down, 100);

			//transform.position = Snap();
			transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }

	public void CreateNewPath()
	{
		StartCoroutine(NewPath());
	}

	private Vector3 Snap()
	{
		var t = new Vector3();

		//if the target's x is further away, move in the x, else move z
		if(distance.x > distance.y)
		{
			t.x = Round(transform.position.x);
			t.z = transform.position.z;
		}
		else if(distance.x < distance.y)
		{
			t.x = transform.position.x;
			t.z = Round(transform.position.z);
		}

		//y is static
		t.y = maxY;

		//t.x = Round(transform.position.x);
		//t.z = Round(transform.position.z);

		return t;
	}
	
	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}
}
