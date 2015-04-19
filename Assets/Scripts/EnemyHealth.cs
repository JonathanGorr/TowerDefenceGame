using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public GameObject soul;

	void Start () {
		//soul = GameObject.Find("Soul");
	}

	void Update () {
		CheckDeath ();
	}

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "SpikeTrap")
		{
			TakeDamage(1);
		}
		if(col.tag == "Arrow")
		{
			TakeDamage(1);
		}
		if(col.tag == "Acid")
		{
			TakeDamage(1);
		}
	}

	public void TakeDamage (int dmg)
	{
		//lose health based on dmg taken
		health -= dmg;

		CheckDeath ();
	}

	private void CheckDeath ()
	{
		//Check if health total is less than or equal to zero.
		if (health <= 0) 
		{
			GameObject obj = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;

			Destroy(gameObject);
		}
	}
}
