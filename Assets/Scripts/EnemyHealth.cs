using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {

	public GameObject soul;

	public int droppedSouls = 1;

	public override void OnKill()
	{
		//spawns a number of souls
		for(int i = 0; i < droppedSouls; i++)
		{
			GameObject obj = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;
		}

		//this calls the method from the parent into this overriding method
		base.OnKill ();
	}
}
