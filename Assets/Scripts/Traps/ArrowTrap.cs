using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {

	//this script must be attached to a child of the object you want to use it with.

	public Rigidbody Arrow;				
	public float speed = 14f;				
	public float fireRate = 2.4f;

	public AudioClip fire;



	void Start()
	{
		StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
	    yield return new WaitForSeconds(fireRate);

	    Rigidbody arrowInstanceL = Instantiate(Arrow, transform.position, Quaternion.Euler(new Vector3(0,0,0) ) ) as Rigidbody;
			arrowInstanceL.velocity = new Vector3(-speed, 0, 0);

		Rigidbody arrowInstanceR = Instantiate(Arrow, transform.position, Quaternion.Euler(new Vector3(0,0,0) ) ) as Rigidbody;
			arrowInstanceR.velocity = new Vector3(speed, 0, 0);

		SoundManager.instance.PlaySingle (fire);
	    StartCoroutine(Fire());
	}
}
