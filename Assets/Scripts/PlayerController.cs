using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private bool moving;
	private Rigidbody rigidBody;
	private Transform sprite;
	private Animator anim;
	private Camera cam;
	private CharacterController controller;
	private bool
		action;

	private RaycastHit hit;
	private Ray ray;
	public int damage;

	public AudioSource footstep;
	public AudioClip landhard;

	void Awake()
	{
		sprite = transform.Find ("Sprite");
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController>();
		cam = GameObject.Find ("MainCamera").GetComponent<Camera>();
	}

	void Update() {

		//input----------------------------------------------
		action = Input.GetMouseButtonDown (0);

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			footstep.Play();
			//print("walking");

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		else
		{
			footstep.Pause();
			//print("paused");
		}

		//Action----------------------------------------------
		if(action)
		{
			anim.SetTrigger("Attack");
		}

		//if moving-------------------------------------------
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0)
		{
			moving = true;
		}
		else
		{
			moving = false;
		}
		
		//flipping--------------------------------------------
		Vector3 localScale = sprite.transform.localScale;

		if(moving)
		{
			anim.SetInteger("AnimState", 1);

			if(moveDirection.x > 0)
				localScale.x = 1f;
			else if(moveDirection.x < 0)
				localScale.x = -1f;
		}

		//attack on the side the cursor is on realtive to player
		else if(action)
		{
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) {

			//flip if hit
			if(hit.point.x > transform.position.x)
				localScale.x = 1f;
			else if(hit.point.x < transform.position.x)
				localScale.x = -1f;
			}
		}
		else
		{
			anim.SetInteger("AnimState", 0);
		}

		//apply scale
		sprite.localScale = localScale;
		
		//Apply changes---------------------------------------
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}