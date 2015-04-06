using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode Attack1Key = KeyCode.Space;

	public float xspeed = 9f;
	public float zspeed = 4f;
	
	bool standing, walking, walking2,attack1;
	float xDirection;

	Animator animator;
	PlayerController playerControl;

	void Start(){
		animator = GetComponent<Animator> ();
		playerControl = GetComponent<PlayerController>();
	}
	void Update(){
		float absVeloX = Mathf.Abs (GetComponent<Rigidbody>().velocity.x);


		//Aniation

		if (walking) {
			animator.Play (Animator.StringToHash("Walk"));
		}
		else{
			// *** move animation ... 
			animator.Play (Animator.StringToHash("Stop"));
		}

		if (Input.GetKey (rightKey)) {
			walking = true;
			xDirection = 1;
		} else if (Input.GetKey (leftKey))  {
			walking = true;
			xDirection = -1;
		} else if (Input.GetKey (upKey)) {
			walking = true;
		}
		else if (Input.GetKey (downKey)) {
			walking = true;
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
			walking = true;
		}
		else if(Input.GetKey(rightKey)){
			velocity.x = xspeed;
			walking = true;
		}

		// y movement with velocity
		if(Input.GetKey(upKey)){
			velocity.z = zspeed;
			walking2 = true;
		}
		else if(Input.GetKey(downKey)){
			velocity.z = -zspeed;
			walking2 = true;
		}

		GetComponent<Rigidbody>().velocity = velocity;

		//if(standing){
		if (Input.GetKeyDown(Attack1Key)) {
			attack1 = true;
			standing = false;
		}

		//}
		if(attack1){
			playerControl.enabled = false;
			standing = false;
			animator.Play (Animator.StringToHash("Warrior1Attack1"));
			attack1 = false;
		}
	}
	void Attack1(){
		animator.Play (Animator.StringToHash("Stop"));
		playerControl.enabled = true;
		standing = true;
		attack1 = false;
	}
}
