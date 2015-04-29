using UnityEngine;
using System.Collections;

public class SmasherController : PlayerController {

	//spawns and speeds
	private float[] randomX = new float[] {.7f, 1f};
	private int[] randomZ = new int[] {-2, 2};

	//sounds
	public AudioClip[] spawnSounds;

	//rays
	private Transform rayStart, rayEnd;

	//transforms
	private Transform target;

	//[HideInInspector]
	public bool colliding;

	public void Start () {

		//rays
		rayStart = transform.Find ("RayStart").transform;
		rayEnd = transform.Find ("RayEnd").transform;

		//components
		rigidBody = GetComponent<Rigidbody> ();

		target = GameObject.FindGameObjectWithTag ("Heart").transform;

		//sound
		if(SoundManager.instance)
			SoundManager.instance.PlaySingle(spawnSounds[Random.Range(0, spawnSounds.Length)]);
	}

	public override void Update () {
		base.Update ();

		RayCasting ();

		//if not sleeping, is moving; walking
		if(!rigidBody.IsSleeping())
		{
			base.animator.SetInteger("AnimState", 1);
			base.moving = true;
		}
		//else if stopped, attack?
		else if(colliding)
		{
			base.animator.SetTrigger("Attack");
			print("colliding");
		}
		//else stand still
		else
		{
			base.animator.SetInteger("AnimState", 0);
			base.moving = false;
		}

		Vector3 direction = new Vector3(1,0,0);
		if(!colliding)
			transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
	}

	void Attack()
	{
		animator.SetTrigger ("Attack");
	}

	void RayCasting()
	{
		Debug.DrawLine (rayStart.position, rayEnd.position, Color.yellow);
		colliding = Physics2D.Linecast(rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer ("Cubes"));
	}
}

