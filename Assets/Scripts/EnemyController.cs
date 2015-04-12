using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public float speed, walkSpeed = 6.0F, attackSpeed = 0f, attackDelay = 2f;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public GameObject target;
	//private CharacterController controller;
	private float xDir, zDir;
	private bool moving, aggro, canAttack = true;
	private Rigidbody rigidBody;
	private float distance;
	public float attackDistance = 2f;
	private Slider targetHealth;
	private Transform sprite;
	private Animator anim;
	public int attackValue = 5;

	private void Awake()
	{
		anim = GetComponentInChildren<Animator> ();
		sprite = transform.Find ("Sprite");
		rigidBody = GetComponent<Rigidbody>();
		//controller = GetComponent<CharacterController>();
		target = GameObject.FindGameObjectWithTag("Heart"); //insert object of importance

		if(!target)
			target = GameObject.Find("Player");

		targetHealth = GameObject.Find ("Health").GetComponent<Slider> ();
		//targetHealth = target.GetComponentInChildren<Slider>();
	}

	void Update() {

		MoveEnemy ();

		/*
		//jump if grounded and stopped
		if (controller.isGrounded) {
			if (rigidBody.velocity.magnitude < 0.1f)
				moveDirection.y = jumpSpeed;
		}
		*/

		//if not sleeping, is moving; walking
		if(!rigidBody.IsSleeping())
		{
			anim.SetInteger("AnimState", 1);
			moving = true;
		}
		else
		{
			anim.SetInteger("AnimState", 0);
			moving = false;
		}

		//distance is the difference between the target and enemy location
		distance = Vector3.Distance(target.transform.position, transform.position);
		float direction = target.transform.position.x - transform.position.x;

		//flipping--------------------------------------------------
		Vector3 localScale = transform.localScale;

		if (direction > 0)
			localScale = new Vector3(1,1,1);
		else if (direction < 0)
			localScale = new Vector3(-1,1,1);

		sprite.localScale = localScale;

		//----------------------------------------------------------

		moveDirection.y -= gravity * Time.deltaTime;//gravity
		//controller.Move(moveDirection * speed * Time.deltaTime);

		//within attack range
		if(distance <= attackDistance && canAttack)
		{
			StartCoroutine("Attack");
		}
	}

	private void MoveEnemy()
	{
		//if the target's z position is greater than the x position away, move in that direction, else vice versa
		if (Mathf.Abs (target.transform.position.x - transform.position.x) < Mathf.Abs(target.transform.position.z - transform.position.z))
		{
			zDir = target.transform.position.z > transform.position.z ? 1 : -1; //if true: move up, else move down
			xDir = 0;
		}
		else
		{
			xDir = target.transform.position.x > transform.position.x ? 1 : -1;
			zDir = 0;
		}

		moveDirection.x = xDir;
		moveDirection.z = zDir;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		if(target)
			Gizmos.DrawLine (transform.position, target.transform.position);
	}

	private IEnumerator Attack()
	{
		canAttack = false;
		anim.SetTrigger ("Attack");
		targetHealth.value -= attackValue;
		yield return new WaitForSeconds(attackDelay);
		canAttack = true;
	}
}