using UnityEngine;
using System.Collections;

public class KeyboardRigidbody : MonoBehaviour {

	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;

	public float xspeed = 9f;
	public float zspeed = 4f;

	bool standing, walking;
	float xDirection;

	Animator animator;
	void Start(){
		animator = GetComponent<Animator> ();
	}
	void Update(){
		float absVeloX = Mathf.Abs (GetComponent<Rigidbody>().velocity.x);

		//Aniation
		if(standing){ // If the player is grounded...
			// Trigger animation state in Animator
			if(absVeloX < 1f){ // ... and (basically) not moving on x ...
				// *** idle animation ...
				animator.Play (Animator.StringToHash("Stop"));
			}
			else{
				// *** move animation ... 
				animator.Play (Animator.StringToHash("Walk"));
			}
		}

		if(Input.GetKey(rightKey)){
			walking = true;
			xDirection = 1;
		}
		else if (Input.GetKey(leftKey)){
			walking = true;
			xDirection = -1;
		}
		else{
			walking = false;
			xDirection = 0;
		}
		
	}

	void FixedUpdate () {
		// Stop movement when there is no input
		Vector3 velocity = new Vector3(0,0,0);
		Vector3 localScale = transform.localScale;
		if(walking){
			if(xDirection > 0){
				localScale.x = 1f; // Flip the player to face right
			}
			else if (xDirection < 0){
				localScale.x = -1f; // // Flip the player to face left
			}
		}
		velocity.x = xspeed * xDirection; // Move
		transform.localScale = localScale;

		// x movement with velocity
		if(Input.GetKey(leftKey)){
			velocity.x = -xspeed;

		}
		else if(Input.GetKey(rightKey)){
			velocity.x = xspeed;
			animator.Play (Animator.StringToHash("Walk"));
		}
		else{
			animator.Play (Animator.StringToHash("Stop"));
		}

		// y movement with velocity
		if(Input.GetKey(upKey)){
			velocity.z = zspeed;
		}
		else if(Input.GetKey(downKey)){
			velocity.z = -zspeed;
		}

		GetComponent<Rigidbody>().velocity = velocity;

	}
}
