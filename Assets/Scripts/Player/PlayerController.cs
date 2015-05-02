using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//values
	public float
		speed = 6.0F,
		jumpSpeed = 8.0F,
		gravity = 20.0F,
		footStepSpeed = .2f,
		knockBackHorizontalF = 4f,
		knockBackVerticalF = 4f;

	//vectors
	private Vector3 moveDirection = Vector3.zero , axis;

	//transforms
	private Transform sprite;

	//components
	[HideInInspector]
	public Animator animator;
	[HideInInspector]
	public Rigidbody rigidBody;
	private CharacterController controller;
	private PlayerInput input;

	//bools
	[HideInInspector]
	public bool action, moving, jump;
	private bool step, left;

	//raycasting
	private RaycastHit hit;
	private Ray ray;

	//audio
	private AudioSource source;
	public AudioClip landhard;
	public AudioClip[] footSteps;

	public virtual void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		input = GetComponent<PlayerInput> ();
		source = GetComponent<AudioSource> ();
		sprite = transform.Find ("Sprite");
		animator = GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController>();
	}

	public virtual void Update() {
		
		//if this is the player
		if (gameObject.tag == "Player") {

			//if the input script is attached
			if (input) {
				action = input.action;
				moving = input.moving;
				jump = input.jump;
				axis = input.axis;
			}
		}

		//flipping--------------------------------------------
		Vector3 localScale = sprite.transform.localScale;

		//grounded
		if (controller.isGrounded) {

			//assign values
			moveDirection = axis;
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			if (jump)
			{
				moveDirection.y = jumpSpeed;
			}

			//moving
			if (moving) {
				animator.SetInteger ("AnimState", 1);

				//flipping...
				if (moveDirection.x > 0)
				{
					left = false;
					localScale.x = 1f;
				}
				else if (moveDirection.x < 0)
				{
					left = true;
					localScale.x = -1f;
				}

				if(!step)
					StartCoroutine("FootSteps", footStepSpeed);
			}
			//else not moving
			else
			{
				animator.SetInteger ("AnimState", 0);
			}

			/*
			if(hit)
			{
				KnockBack();
			}
			*/

			//attack on the side the cursor is on relative to player
			if (action)
			{
				//play attack animation
				animator.SetTrigger("Attack");

				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {

					//flip if hit
					if (hit.point.x > transform.position.x)
						localScale.x = 1f;
					else if (hit.point.x < transform.position.x)
						localScale.x = -1f;
				}
			}
		}
		//else the object is not grounded
		else
		{
			//play jump animation
			animator.SetInteger("AnimState", 3);
			source.Pause();
		}

		//apply scale
		sprite.localScale = localScale;
		
		//apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		//move
		controller.Move(moveDirection * Time.deltaTime);
	}

	public virtual void KnockBack()
	{
		if(left)
		{
			moveDirection.x = knockBackHorizontalF;
		}
		else
		{
			moveDirection.x = -knockBackHorizontalF;
		}
		
		moveDirection.y = knockBackVerticalF;
	}

	//plays a random footstep sound at an interval
	IEnumerator FootSteps(float interval)
	{
		step = true;
		SoundManager.instance.PlaySingle(footSteps[Random.Range(0, footSteps.Length)]);
		yield return new WaitForSeconds (interval);
		step = false;
	}
}