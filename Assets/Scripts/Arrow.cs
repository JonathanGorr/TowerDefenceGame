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
		if(col.tag == "Creeper" || col.tag == "Crawler" || col.tag == "EnemyHitbox" || col.tag == "Enemy" || col.tag == "Ground")
		{
			Destroy(gameObject);
		}
	}
}
