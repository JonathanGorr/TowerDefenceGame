using UnityEngine;
using System.Collections;

public class SmasherController : MonoBehaviour {

	public float xspeed;
	public float waitRate = 3f;
	public float waitDuration = 1f;

	private float[] randomX = new float[] {.7f, 1f};
	private int[] randomZ = new int[] {-2, 2};

	private Rigidbody rigidbody;
	//public AudioClip creeperdmg1;
	//public AudioClip creeperdmg2;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		xspeed = randomX[Random.Range(0, randomX.Length)];
	}

	void Update () {
	}
	
	void FixedUpdate () {
		rigidbody.velocity = new Vector3(xspeed, 0f, 0f);
	}



	IEnumerator StutterStep()
	{
	    yield return new WaitForSeconds(waitRate);
	    	xspeed = 0f;
	    	//SoundManager.instance.RandomizeSfx (smasheryell1, smasheryell2);

	     yield return new WaitForSeconds(waitDuration);
	    	xspeed = randomX[Random.Range(0, randomX.Length)];

	    StartCoroutine(StutterStep());
	}


	public void TakeDamage ()
	{
		//SoundManager.instance.RandomizeSfx (creeperdmg1, creeperdmg2);
	}

	private void Death ()
	{
		//SoundManager.instance.RandomizeSfx (smasherdeath1, smasherdeath2);
	}

}

