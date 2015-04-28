using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {

	//transforms
	public GameObject soul;

	//values
	public float verticalForce = 300f, horizontalForce = 50f;
	public int droppedSouls = 1;
	
	public override void OnKill()
	{
		//spawns a number of souls
		for(int i = 0; i < droppedSouls; i++)
		{
			GameObject obj = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;

			//throw the soul vertically
			obj.GetComponent<Rigidbody>().AddForce(Vector3.up * verticalForce);

			//pick 0 or 1
			int direction = Random.Range(0,1);

			//throw left or right depending
			if(direction == 0)
				obj.GetComponent<Rigidbody>().AddForce(Vector3.left * horizontalForce);
			else
				obj.GetComponent<Rigidbody>().AddForce(Vector3.right * horizontalForce);
		}

		//this calls the method from the parent into this overriding method
		base.OnKill ();
	}
}
