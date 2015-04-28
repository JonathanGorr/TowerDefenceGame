using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//values
	public float 
		speed = 6.0F,
		jumpSpeed = 8.0F,
		gravity = 20.0F,
		footStepSpeed = .2f;

	//vectors
	private Vector3 moveDirection = Vector3.zero , axis;

	//transforms
	private Transform sprite;

	//components
	[HideInInspector]
	public Animator animator;
	[HideInInspector]
	public Rigidbody rigidBody;
	private Camera cam;
	private CharacterController controller;
	private PlayerInput input;

	//bools
	[HideInInspector]
	public bool action, moving, jump;

	private bool step;

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
		cam = GameObject.Find ("MainCamera").GetComponent<Camera>();
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

		//if grounded...
		if (controller.isGrounded) {
			moveDirection = axis;
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			source.Play();

			if (jump)
			{
				moveDirection.y = jumpSpeed;
			}
		}
		//else if not grounded...
		else
		{
			animator.SetInteger("AnimState", 3);
			source.Pause();
		}

		//Action----------------------------------------------
		if(action)
		{
			animator.SetTrigger("Attack");
		}
		
		//flipping--------------------------------------------
		Vector3 localScale = sprite.transform.localScale;

		//grounded
		if (controller.isGrounded) {
			//moving
			if (moving) {
				animator.SetInteger ("AnimState", 1);

				//flipping...
				if (moveDirection.x > 0)
					localScale.x = 1f;
				else if (moveDirection.x < 0)
					localScale.x = -1f;

				if(!step)
					StartCoroutine("FootSteps", footStepSpeed);
			}

			//attack on the side the cursor is on relative to player
			else if (action)
			{
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {

					//flip if hit
					if (hit.point.x > transform.position.x)
						localScale.x = 1f;
					else if (hit.point.x < transform.position.x)
						localScale.x = -1f;
				}
			}
			//else not moving
			else
			{
				animator.SetInteger ("AnimState", 0);
			}
		}

		//apply scale
		sprite.localScale = localScale;
		
		//Apply changes---------------------------------------
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	IEnumerator FootSteps(float interval)
	{
		step = true;
		SoundManager.instance.PlaySingle(footSteps[Random.Range(0, footSteps.Length)]);
		yield return new WaitForSeconds (interval);
		step = false;
	}
}