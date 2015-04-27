using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

	//components
	private TDManager tdManager;
	private TDEnemy tdEnemy;
	[HideInInspector]
	public Rigidbody rigidBody;
	private EnemyAttack enemyAttack;
	[HideInInspector]
	public Animator animator;
	
	//transforms
	private Transform player;
	private Transform heart;
	private Transform sprite;
	
	//vectors
	public Vector3 chaseRange = new Vector3 (4, 4, 4), attackRange = new Vector3 (2, 2, 2);
	private Vector3	
		localScale,
		targetDistance;
	
	//bools
	private bool
		canAttack = true,
		pathMover = true,
		newPath = true;

	[HideInInspector]
	public bool attacking, moving;
	
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
	public enum States{ Seeking, Chasing, Attacking, Dead }

	public virtual void Awake()
	{
		enemyAttack = GetComponentInChildren<EnemyAttack> ();
		rigidBody = GetComponent<Rigidbody> ();
		sprite = transform.Find ("Sprite").transform;
		heart = GameObject.FindGameObjectWithTag ("Heart").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		animator = GetComponentInChildren<Animator> ();
		tdManager = GameObject.Find ("LevelManager").GetComponent<TDManager>();
		tdEnemy = GetComponent<TDEnemy> ();

		if (tdEnemy) {
			//if the target is unnassigned, heart is target by default
			if (!tdEnemy.target) {
				tdEnemy.target = heart;
			}
		} else
			print ("there is no tdEnemy Script");
	}

	public virtual void Update()
	{
		//run the state machine each frame
		State ();

		//attacking if the state is attacking
		if (currentState == States.Attacking)
			attacking = true;
		else
			attacking = false;

		//if not sleeping, is moving; walking
		if (rigidBody) 
		{
			if (!rigidBody.IsSleeping())
			{
				animator.SetInteger ("AnimState", 1);
				moving = true;
			} 
			else
			{
				animator.SetInteger ("AnimState", 0);
				moving = false;
			}
		}

		//BEHAVIOR:
		//The enemy seeks the heart by default
		//if the player is within chase range, set player as target
		//if player is then out of chase range, heart is target
		//if any vulnerable object is within attack range, attack

		//Distances----------------------------------------------------
		if(player)
		{
			//get the distance between the player and this enemy
			Vector3 distanceToPlayer = player.transform.position - transform.position;

			//if the player is close enough, chase
			if(Mathf.Abs(distanceToPlayer.x) < chaseRange.x && Mathf.Abs(distanceToPlayer.z) < chaseRange.z)
			{
				//print("chasing");
				tdEnemy.target = player;
				currentState = States.Chasing;
			}

			//else if the player is within attack range, attack
			else if(Mathf.Abs(distanceToPlayer.x) < attackRange.x && Mathf.Abs(distanceToPlayer.z) < attackRange.z)
			{
				//print("attacking");
				tdEnemy.target = player;
				currentState = States.Attacking;
			}

			//else the player is too far away, seek the objective
			else
			{
				//print("seeking objective");
				if(tdEnemy)
					tdEnemy.target = heart;

				currentState = States.Seeking;
			}
		}
		
		//if there is a target, get target distance
		if (tdEnemy) 
		{
			if (tdEnemy.target) {
				targetDistance = tdEnemy.target.position - transform.position;
			}
		}

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
		
		//Flipping-------------------------------------------------------------
		Vector3 localScale = transform.localScale;
		
		if(targetDistance.x > 0)
			localScale.x = 1f;
		else if(targetDistance.x < 0)
			localScale.x = -1f;

		//apply these changes back to the object
		transform.localScale = localScale;
	}

	public virtual void State()
	{
		// This if() makes sure that each state only runs when it is entered. Since this example is using IEnumerator functions and Animator triggers, running the code only once is critical
		// switch case conditional which uses currentState's value
		switch (currentState) {
		case States.Seeking:
			StartCoroutine (Seeking (.5f));
			break;
			
		case States.Chasing:
			StartCoroutine (Chasing (.05f));
			break;
			
		case States.Attacking:
			StartCoroutine (Attacking (.05f));
			break;
			
		case States.Dead:
			Dead ();
			break;
		}
	}

	IEnumerator Seeking(float interval)
	{
		//walking
		animator.SetInteger ("AnimState", 1);

		if (tdEnemy) 
		{
			if (tdEnemy.newPath) {
				tdEnemy.StartTimer ();
			}

			tdEnemy.Movement();
		}
		
		yield return new WaitForSeconds(interval);
	}
	
	IEnumerator Chasing(float interval)
	{
		//walking
		animator.SetInteger ("AnimState", 1);

		if (tdEnemy.newPath)
		{
			tdEnemy.StartTimer();
		}
		
		tdEnemy.Movement();
		
		yield return new WaitForSeconds(interval);
	}
	
	IEnumerator Attacking(float interval){

		//walking
		animator.SetInteger ("AnimState", 0);

		//if can attack, attack
		if(canAttack)
		{
			if(currentState != States.Seeking)
			{
				animator.SetTrigger("Attack");
			}
		}
		
		yield return new WaitForSeconds(interval);
	}

	//this delays the attack- cannot attack as fast as the animation can loop
	IEnumerator Delay()
	{
		canAttack = false;
		yield return new WaitForSeconds (delay);
		canAttack = true;
	}

	IEnumerator Dead()
	{
		animator.SetTrigger("Die");
		Destroy (gameObject);

		yield return new WaitForEndOfFrame();
	}
}
