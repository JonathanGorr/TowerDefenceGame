using UnityEngine;
using System.Collections;

public class PlayerControllerUnityGravity : MonoBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero, mousePos;
	private bool moving;
	private Rigidbody rigidBody;
	private Transform sprite;
	private Animator anim;
	private Camera cam;
	private Building building;
	private CharacterController controller;
	private LevelManager manager;
	private bool
		action;

	void Awake()
	{
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		sprite = transform.Find ("Sprite");
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController>();
		cam = GameObject.Find ("MainCamera").GetComponent<Camera>();
		building = GameObject.Find ("LevelManager").GetComponent<Building> ();
	}

	void Update() {

		//get the relative distance between the world mouse pos and the player
		mousePos = transform.position - building.mousePos;
		//print (mousePos);

		//input----------------------------------------------
		action = Input.GetMouseButtonDown (0);

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}

		//Action----------------------------------------------
		if(action)
		{
			anim.SetTrigger("Attack");
		}

		//if moving-------------------------------------------
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)//controller.velocity != Vector3.zero)
			moving = true;
		else
			moving = false;

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
		else if(action)
		{
			print("attacking");

			if(mousePos.x < 0)
				localScale.x = 1f;
			else if(mousePos.x > 0)
				localScale.x = -1f;
		}
		else
			anim.SetInteger("AnimState", 0);

		sprite.localScale = localScale;
		
		//----------------------------------------------------
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}