using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public float speed, walkSpeed = 6.0F, attackSpeed = 0f, attackDelay = 2f;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Transform target;
	private CharacterController controller;
	private float xDir, zDir;
	private bool moving, aggro, canAttack = true;
	private Rigidbody rigidBody;
	private Vector3 distance;
	public Vector3 attackDistance;
	private Slider targetHealth;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		controller = GetComponent<CharacterController>();
		target = GameObject.FindGameObjectWithTag("Heart").transform; //insert object of importance
		targetHealth = target.GetComponentInChildren<Slider>();
	}

	void Update() {

		MoveEnemy ();
		
		//jump if grounded and stopped
		if (controller.isGrounded) {
			if(rigidBody.velocity.magnitude < 0.1f)
				moveDirection.y = jumpSpeed;
		}


		//distance is the difference between the target and enemy location
		distance = target.transform.position - transform.position;

		//within attack range
		if(distance.x <= attackDistance.x && distance.z <= attackDistance.y && canAttack)
		{
			StartCoroutine("Attack");
		}

		//flipping
		Vector3 localScale = transform.localScale;

		if (moveDirection.x < 0)
			localScale.x = 1f;
		else if (moveDirection.x > 0)
			localScale.x = -1f;

		transform.localScale = localScale;

		moveDirection.y -= gravity * Time.deltaTime;//gravity
		controller.Move(moveDirection * speed * Time.deltaTime);
	}

	private void MoveEnemy()
	{
		//if the target's z position is greater than the x position away, move in that direction, else vice versa
		if (Mathf.Abs (target.position.x - transform.position.x) < Mathf.Abs(target.position.z - transform.position.z))
		{
			zDir = target.position.z > transform.position.z ? 1 : -1; //if true: move up, else move down
			xDir = 0;
		}
		else
		{
			xDir = target.position.x > transform.position.x ? 1 : -1;
			zDir = 0;
		}

		moveDirection.x = xDir;
		moveDirection.z = zDir;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		if(target)
			Gizmos.DrawLine (transform.position, target.position);
	}

	private IEnumerator Attack()
	{
		canAttack = false;
		targetHealth.value --;
		yield return new WaitForSeconds(attackDelay);
		canAttack = true;
	}
}