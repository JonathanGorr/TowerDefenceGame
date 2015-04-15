using UnityEngine;
using System.Collections;

public class AcidTrap : MonoBehaviour {

	//this script must be attached to a child of the object you want to use it with.

	public Rigidbody Acid;				
	public float speed = 6f;				
	public float fireRate = 3f;

	void Start()
	{
		
        StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
	    yield return new WaitForSeconds(fireRate);

	    Rigidbody acidInstance = Instantiate(Acid, transform.position, Quaternion.Euler(new Vector3(0,0,0) ) ) as Rigidbody;
			acidInstance.velocity = new Vector3(0, -speed, 0);

	    StartCoroutine(Fire());
	}
}
