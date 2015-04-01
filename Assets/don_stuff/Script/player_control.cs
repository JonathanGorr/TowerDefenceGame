using UnityEngine;
using System.Collections;

public class player_control : MonoBehaviour {
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode stabKey = KeyCode.S;
	public float speed =5f;
	public float jumpVelocity = 30f;
	
	public int maxJumpCount = 2;
	public int jumpCount;
	
	bool standing, walking, jumping, stab;

	float xDirection;

	[HideInInspector]
	Animator animator;
	player_control playerControl;


	void Start(){
		playerControl = GetComponent<player_control>();
		animator = GetComponent<Animator> ();
	}
	//NEW:SET 
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Platform"){
			jumpCount = 0;	
			standing = true;

			playerControl.enabled = true;
		}
	}
	
	void Update(){
		float absVeloY = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.y);
		float absVeloX = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x);
		
		if(absVeloY >= .5f){
			standing = false;
		}
		//error___________________________________________
		if (maxJumpCount == 1){
			if(Input.GetKeyDown (jumpKey) && standing){
			jumping = true; 
			}
	    }
		//Aniation
		if(standing){ // If the player is grounded...
			// Trigger animation state in Animator
			if(absVeloX < 1f){ // ... and (basically) not moving on x ...
				// *** idle animation ...
				animator.Play (Animator.StringToHash("pan_stop"));
				stab = false;
			}
			else{
				// *** move animation ... 
				animator.Play (Animator.StringToHash("pan_walk"));
			}
		}
		else{ // If the player is not grounded...
			// *** jump animation ... 
			animator.Play (Animator.StringToHash("pan_jump"));
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
	
	
	//error-----------------------------------------------
	void FixedUpdate () {
		Vector3 localScale = transform.localScale;
		
		
		Vector2 velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y); // 0 to stop x when no input is given. Keep current y velocity
		
		if(jumping){
			jumpCount++;
			velocity.y = jumpVelocity;
			jumping = false;
		}
		
	
		if(walking){
			if(xDirection > 0){
				localScale.x = -1f; // Flip the player to face right
			}
			else if (xDirection < 0){
				localScale.x = 1f; // // Flip the player to face left
			}
		}
		velocity.x = speed * xDirection; // Move

		transform.localScale = localScale;
		GetComponent<Rigidbody2D>().velocity = velocity; // Apply changes to physics

		if(standing){
			if (Input.GetKeyDown(stabKey)) {
				stab = true;
				standing = false;
			}
	 	}
		if(stab){
			playerControl.enabled = false;
			jumping = false;
			standing = false;
			animator.Play (Animator.StringToHash("pan_stab"));
			stab = false;
		}

	}
	void STAB(){
		animator.Play (Animator.StringToHash("pan_stop"));
		playerControl.enabled = true;
		standing = true;
		stab = false;
		}
	void Restore(){
		playerControl.enabled = true;
		standing = true;
		stab = false;
	}
}