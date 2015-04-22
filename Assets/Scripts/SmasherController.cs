using UnityEngine;
using System.Collections;

public class SmasherController : MonoBehaviour {

	public float xspeed;
	public float waitRate = 3f;
	public float waitDuration = 1f;

	private float[] randomX = new float[] {.7f, 1f};
	private int[] randomZ = new int[] {-2, 2};

	private Rigidbody rigidBody;

	public AudioClip crusherdeath1;
	public AudioClip crusherdeath2;

	private Animator animator;

	private bool moving;

	public virtual void Awake () {
		animator = GetComponentInChildren<Animator> ();
		rigidBody = GetComponent<Rigidbody>();
		xspeed = randomX[Random.Range(0, randomX.Length)];

		if(SoundManager.instance)
			SoundManager.instance.RandomizeSfx (crusherdeath1, crusherdeath2);
	}
	
	void FixedUpdate () {

		//if not sleeping, is moving; walking
		if(!rigidBody.IsSleeping())
		{
			animator.SetInteger("AnimState", 1);
			moving = true;
		}
		else
		{
			animator.SetInteger("AnimState", 0);
			moving = false;
		}

		Vector3 direction = new Vector3(1,0,0);
		transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * xspeed);
	}

	/*IEnumerator StutterStep()
	{
	    yield return new WaitForSeconds(waitRate);
	    	xspeed = 0f;

	     yield return new WaitForSeconds(waitDuration);
	    	xspeed = randomX[Random.Range(0, randomX.Length)];

	    StartCoroutine(StutterStep());
	}*/
	
	public void TakeDamage ()
	{
		//SoundManager.instance.RandomizeSfx (creeperdmg1, creeperdmg2);
	}

	private void Death ()
	{
		//SoundManager.instance.RandomizeSfx (crusherdeath1,crusherdeath2);
	}

}

