using UnityEngine;
using System.Collections;

public class TDEnemy : Pathfinding
{
	private TDManager tdManager;
	public Vector3 spawn;
	public Transform target;
	private Transform player;
	private Transform heart;
	private Transform sprite;
	private Vector3 targetDistance;
	private Vector3 localScale;
	public int damage;
	private Rigidbody rigidBody;
	private bool moving;

	private Vector3 
		chaseRange = new Vector3 (2, 2, 2),
		attackRange = new Vector3 (1,1,1);

	[HideInInspector]
	public GameObject closest;

    private bool pathMover = true;
    private bool newPath = true;

	public float 
		seekSpeed = 1f,
		chaseSpeed = 2f;

	//state machine
	private States currentState = States.Seeking;
	private States lastState = States.Dead;
	private Animator animator;

	private enum States{
		Seeking,
		Chasing,
		Attacking,
		Dead
	}

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		sprite = transform.Find ("Sprite").transform;
		heart = GameObject.FindGameObjectWithTag ("Heart").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		animator = GetComponentInChildren<Animator> ();
		tdManager = GameObject.Find ("LevelManager").GetComponent<TDManager>();
	}

	private void Update()
	{
		//if not sleeping, is moving; walking
		if(!rigidBody.IsSleeping())
		{
			animator.SetInteger("AnimState", 1);
			moving = true;
		}
		else
		{
			animator.SetInteger("AnimState", 0);
			moving = false;
		}

		// This if() makes sure that each state only runs when it is entered. Since this example is using IEnumerator functions and Animator triggers, running the code only once is critical
		// switch case conditional which uses currentState's value
		switch(currentState){

			// If the switch condition matches this case then do this...
		case States.Seeking:
			animator.SetTrigger ("Seeking");
			StartCoroutine(Seeking(.5f)); // Run the IEnumerator function with StartCoroutine
			break; // don't forget to add break for each case or the next case will also execute!

		case States.Chasing:
			animator.SetTrigger ("Attacking");
			StartCoroutine(Attacking(.05f));
			break;
			
		case States.Attacking:
			animator.SetTrigger ("Attacking");
			StartCoroutine(Attacking(.05f));
			break;
			
		case States.Dead:
			Dead ();
			break;
		}

		//currentState = States.Attacking;

		print(currentState);

		// Update lastState to whatever currentState was set to
		//lastState = currentState;

		Vector3 distanceToPlayer = player.transform.position - transform.position;
		targetDistance = target.position - transform.position;

		//if the target is within distance, attack
		if(targetDistance.x < attackRange.x && targetDistance.z < attackRange.z)
		{
			currentState = States.Attacking;
		}
		else
			currentState = States.Seeking;

		//Flipping-------------------------------------------------------------
		Vector3 localScale = transform.localScale;

		if(targetDistance.x > 0)
			localScale.x = 1f;
		else if(targetDistance.x < 0)
			localScale.x = -1f;

       	transform.localScale = localScale;
	}

    IEnumerator PathTimer()
    {
        newPath = false;
        FindPath(transform.position, target.transform.position);
        yield return new WaitForSeconds(0.5F);
        newPath = true;
    }

    private void Movement(float speed)
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

	IEnumerator Seeking(float interval)
	{
		spawn = tdManager.spawn.transform.position;
		//target = tdManager.target.transform;

		//TODO:
		//set this to absolute distance greater than attackdistance
		if (transform.position.x < 10.2F)
		{
			if (newPath)
			{
				StartCoroutine(PathTimer());
			}
			
			Movement(seekSpeed);
		}
		else
		{
			DestroyImmediate(gameObject);
		}

		yield return new WaitForSeconds(interval);
	}
	
	IEnumerator Chasing(float interval){
		while(true){

			Movement(chaseSpeed);

			yield return new WaitForSeconds(interval);
		}
	}

	IEnumerator Attacking(float interval){
		while(true){

			animator.SetTrigger("Attack");

			yield return new WaitForSeconds(interval);
		}
	}
	
	void Dead(){
		Destroy (gameObject);
	}
}