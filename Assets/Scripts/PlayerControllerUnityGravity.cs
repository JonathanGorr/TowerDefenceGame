using UnityEngine;
using System.Collections;

public class PlayerControllerUnityGravity : MonoBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private bool moving;
	private Rigidbody rigidBody;
	private Transform sprite;

	void Update() {

		sprite = transform.Find ("Sprite");
		rigidBody = GetComponent<Rigidbody> ();
		CharacterController controller = GetComponent<CharacterController>();

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			}

		//if moving
		if (rigidBody.velocity.magnitude > 0) {
			moving = true;
		} else
			moving = false;

		//flipping
		Vector3 localScale = sprite.transform.localScale;

		if(moving)
		{
			if(moveDirection.x > 0){
				localScale.x = 1f; // Flip the player to face right
			}
			else if(moveDirection.x < 0){
				localScale.x = -1f; // // Flip the player to face left
			}
		}

		sprite.transform.localScale = localScale;
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}