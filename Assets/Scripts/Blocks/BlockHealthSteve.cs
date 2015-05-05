using UnityEngine;
using System.Collections;

public class BlockHealthSteve : MonoBehaviour {

	public int health;

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Creeper" || col.tag == "Enemy")
		{
			LoseHealth(1);
			StartCoroutine (WaitDamageCreeper());
		}
		if(col.tag == "Crusher")
		{
			LoseHealth(2);
			StartCoroutine (WaitDamageCrusher());
		}
		/*if(col.tag == "Acid")
		{
			LoseHealth(1);
		}*/
	}

	private void OnTriggerExit (Collider col)
	{
		if(col.tag == "Creeper" || col.tag == "Enemy")
		{
			StopCoroutine (WaitDamageCreeper());
		}
		if(col.tag == "Crusher")
		{
			StopCoroutine (WaitDamageCrusher());
		}

	}


	IEnumerator WaitDamageCreeper ()
	{
		yield return new WaitForSeconds (2);
		LoseHealth(1);
		StartCoroutine (WaitDamageCreeper());
	}

	IEnumerator WaitDamageCrusher ()
	{
		yield return new WaitForSeconds (1);
		LoseHealth(1);
		StartCoroutine (WaitDamageCrusher());
	}

	public void LoseHealth (int hit)
	{
		health -= hit;
		UpdateHealth();
	}

	void UpdateHealth() 
	{
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}

}
