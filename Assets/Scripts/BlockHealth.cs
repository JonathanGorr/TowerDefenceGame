using UnityEngine;
using System.Collections;

public class BlockHealth : MonoBehaviour {

	public int health;
	private bool blockhit;
	
	// Update is called once per frame
	void Update () {
		UpdateHealth();
	}



	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Creeper")
		{
			blockhit = true;
			LoseHealth(1);
		}
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
