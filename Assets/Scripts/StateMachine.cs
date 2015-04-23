using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

	//components
	private TDManager tdManager;
	private TDEnemy tdEnemy;
	private Rigidbody rigidBody;
	private EnemyAttack enemyAttack;
	private Animator animator;
	
	//transforms
	private Transform player;
	private Transform heart;
	private Transform sprite;
	
	//vectors
	public Vector3 spawn;
	private Vector3
		chaseRange = new Vector3 (3, 3, 3),
		attackRange = new Vector3 (2, 2, 2),
		localScale,
		targetDistance;
	
	//bools
	private bool 
		canAttack = true,
		moving,
		pathMover = true,
		newPath = true;
	public bool attacking;
	
	//values
	public float 
		seekSpeed = 1f,
		chaseSpeed = 2f,
		delay = 1f;
	
	[HideInInspector]
	public GameObject closest;
	
	//state machine
	[HideInInspector]
	public States currentState = States.Seeking, lastState = States.Dead;
	
	//states
	[HideInInspector]
	public enum States{Seeking, Chasing, Attacking, Dead}

	void Awake()
	{
		enemyAttack = GetComponentInChildren<EnemyAttack> ();
		rigidBody = GetComponent<Rigidbody> ();
		sprite = transform.Find ("Sprite").transform;
		heart = GameObject.FindGameObjectWithTag ("Heart").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		animator = GetComponentInChildren<Animator> ();
		tdManager = GameObject.Find ("LevelManager").GetComponent<TDManager>();
		tdEnemy = GetComponent<TDEnemy> ();
		
		//if the target is unnassigned, heart is target by default
		if(!tdEnemy.target)
		{
			tdEnemy.target = heart;
		}
	}

	private void Update()
	{
		//run the state machine each frame
		State ();

		//attacking if the state is attacking
		if (currentState == States.Attacking)
			attacking = true;
		else
			attacking = false;

		//if not sleeping, is moving; walking
		if (rigidBody) {
			if (!rigidBody.IsSleeping ()) {
				animator.SetInteger ("AnimState", 1);
				moving = true;
			} else {
				animator.SetInteger ("AnimState", 0);
				moving = false;
			}
		} else
			print ("there is no rigidBody");

		//Distances----------------------------------------------------
		if(player)
		{
			Vector3 distanceToPlayer = player.transform.position - transform.position;
		}
		else
			print ("there is no player");
		
		//if there is a target, get target distance
		if (tdEnemy.target) 
		{
			targetDistance = tdEnemy.target.position - transform.position;
		} 
		else
			print ("there is no target");
		
		//AI based on distances----------------------------------------
		if(targetDistance.x < attackRange.x && targetDistance.z < attackRange.z)
		{
			currentState = States.Attacking;
		}
		//else if the target is too far away, resume seeking state
		else
			currentState = States.Seeking;
		
		//TODO:
		//if the target is the player, and has aggroed the enemy, currentState = chaseState, increase speed?
		//if the gameobject is dead, currentState = dead state
		//
		
		//Flipping-------------------------------------------------------------
		Vector3 localScale = transform.localScale;
		
		if(targetDistance.x > 0)
			localScale.x = 1f;
		else if(targetDistance.x < 0)
			localScale.x = -1f;

		//apply these changes back to the object
		transform.localScale = localScale;
	}

	private void State()
	{
		// This if() makes sure that each state only runs when it is entered. Since this example is using IEnumerator functions and Animator triggers, running the code only once is critical
		// switch case conditional which uses currentState's value
		switch (currentState) {
			
			// If the switch condition matches this case then do this...
		case States.Seeking:
			animator.SetTrigger ("Seeking");
			StartCoroutine (Seeking (.5f)); // Run the IEnumerator function with StartCoroutine
			break; // don't forget to add break for each case or the next case will also execute!
			
		case States.Chasing:
			animator.SetTrigger ("Chasing");
			StartCoroutine (Attacking (.05f));
			break;
			
		case States.Attacking:
			animator.SetTrigger ("Attacking");
			StartCoroutine (Attacking (.05f));
			break;
			
		case States.Dead:
			Dead ();
			break;
		}
	}

	IEnumerator Seeking(float interval)
	{
		spawn = tdManager.spawn.transform.position;
		
		animator.SetInteger ("AnimState", 1);
		
		//TODO:
		//set this to absolute distance greater than attackdistance
		if (transform.position.x < 10.2F)
		{
			if (tdEnemy.newPath)
			{
				tdEnemy.StartTimer();
			}

			tdEnemy.Movement(seekSpeed);
		}
		else
		{
			DestroyImmediate(gameObject);
		}
		
		yield return new WaitForSeconds(interval);
	}
	
	IEnumerator Chasing(float interval){
		while(true){
			
			tdEnemy.Movement(chaseSpeed);
			
			yield return new WaitForSeconds(interval);
		}
	}
	
	//this delays the attack- cannot attack as fast as the animation can loop
	IEnumerator Delay()
	{
		canAttack = false;
		yield return new WaitForSeconds (delay);
		canAttack = true;
	}
	
	IEnumerator Attacking(float interval){
		
		while(true){

			//if can attack, attack
			if(canAttack)
			{
				animator.SetTrigger("Attack");
			}
			
			yield return new WaitForSeconds(interval);
		}
	}

	public virtual void Dead(){
		Destroy (gameObject);
	}
}
