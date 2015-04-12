using UnityEngine;
using System.Collections;

public class BlockHealthSteve : MonoBehaviour {

	public int health;
	private bool blockhit;

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Creeper")
		{
			LoseHealth(1);
		}
	}

	void OnTriggerStay (Collider col2)
	{
		if (col2.gameObject.tag == "Creeper")
		{	
			StartCoroutine (WaitDamage());
		}
	}


	IEnumerator WaitDamage ()
	{
		yield return new WaitForSeconds (2);
		LoseHealth(1);
	}

	void Update () {
		//UpdateHealth();
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
