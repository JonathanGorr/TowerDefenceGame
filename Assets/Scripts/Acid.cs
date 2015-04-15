using UnityEngine;
using System.Collections;

public class Acid : MonoBehaviour {


	
	void OnTriggerEnter (Collider col) 
	{
		if(col.tag == "Enemy")
		{
			//col.gameObject.GetComponent<EnemyHealth>().TakeDamage();
			Destroy(gameObject);
		}

		/*if(col.tag == "Cube")
		{
			//col.gameObject.GetComponent<BlockHealth>().LoseHealth();
		}*/

		if(col.tag == "Player")
		{	
			//col.gameObject.GetComponent<PlayerHealth>().LoseHealth();
			Destroy(gameObject);
		}

		if(col.tag == "Ground")
		{
			Destroy(gameObject);
		}
	}
}
