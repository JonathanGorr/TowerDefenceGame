using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {

	//transforms
	public GameObject spawner;

	public int droppedSouls = 1;

	public void Start()
	{
		base.shadow = transform.parent.transform.Find ("Shadow");
	}

	public override void Update()
	{
		base.Update ();

		if(health <= 0)
		{
			base.stateMachine.CurrentState = StateMachine.States.Dead;
		}
	}

	public override void OnKill()
	{
		//Creates a spawner
		//the spawner then executes the drop
		GameObject obj = Instantiate(spawner, transform.position, Quaternion.identity) as GameObject;
		obj.GetComponent<Spawner> ().Spawn(droppedSouls);

		//this calls the method from the parent into this overriding method
		base.OnKill ();
	}
}
