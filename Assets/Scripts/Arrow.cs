using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float duration = .4f;

	void Start () 
	{
		Destroy(gameObject, duration);
	}

	
	void OnTriggerEnter (Collider col) 
	{
		if(col.tag == "Enemy")
		{
			//col.gameObject.GetComponent<EnemyHealth>().TakeDamage();
			Destroy(gameObject);
		}

		if(col.tag == "Ground")
		{
			Destroy(gameObject);
		}
	}
}
