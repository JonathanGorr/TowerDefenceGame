using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Transform target;
	private CharacterController controller;
	private float xDir, zDir;
	private bool moving;
	private Rigidbody rigidBody;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		controller = GetComponent<CharacterController>();
		target = GameObject.Find ("Player").transform; //insert object of importance
	}

	void Update() {

		MoveEnemy ();

		if (controller.isGrounded) {

			//jump if stopped
			if(rigidBody.velocity.magnitude < 0.1f)
				moveDirection.y = jumpSpeed;
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
		if (Mathf.Abs (target.position.x - transform.position.x) < Mathf.Abs(target.position.z - transform.position.z))
			zDir = target.position.z > transform.position.z ? 1 : -1; //if true: move up, else move down
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;

		moveDirection.x = xDir;
		moveDirection.z = zDir;
	}
}