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
		if(col.tag == "Enemy")
		{
			blockhit = true;
			InvokeRepeating("LoseHealth(1)", 1f, 1f);
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
