using UnityEngine;
using System.Collections;

public class AcidTrap : MonoBehaviour {

	//this script must be attached to a child of the object you want to use it with.

	public Rigidbody Acid;				
	public float speed = 6f;
	public float dropspeed = .1f;	
	public float accel = 15f;					
	public float fireRate = 3f;

	private bool active = false;

	public AudioClip drip1;
	public AudioClip drip2;
	public AudioClip drip3;

	void Start()
	{
        StartCoroutine(Fire());
	}

	void FixedUpdate () {
		if (active == true) 
		{
			dropspeed = dropspeed + (accel * Time.deltaTime);
		}
	} 

	IEnumerator Fire()
	{
	    yield return new WaitForSeconds(fireRate/2);
	    active = true;

	    Rigidbody acidInstance1 = Instantiate(Acid, transform.TransformPoint(Vector3.forward), Quaternion.Euler(new Vector3(0,0,0) ) ) as Rigidbody;
			acidInstance1.velocity = new Vector3(0, -speed, 0);

		Rigidbody acidInstance2 = Instantiate(Acid, transform.TransformPoint(Vector3.back), Quaternion.Euler(new Vector3(0,0,0) ) ) as Rigidbody;
			acidInstance2.velocity = new Vector3(0, -speed, 0);

		SoundManager.instance.RandomizeSfx (drip1, drip2, drip3);
		yield return new WaitForSeconds(fireRate/2);
		active = false;
	    StartCoroutine(Fire());
	}
}
