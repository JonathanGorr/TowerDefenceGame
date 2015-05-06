using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectiveHealth : Health {

	private Slider healthBar;

	public void Start()
	{
		healthBar = GetComponentInChildren<Slider>();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public override void Update ()
	{
		healthBar.value = health;

		if(health <= 0)
		{
			manager.DeathScreen();
		}

		base.Update ();
	}

	public override void TakeDamage (int value)
	{
		base.TakeDamage (value);
		print ("Objective took " + value + " damage.");
	}

	public override void OnKill ()
	{
		Application.LoadLevel ("KillScreen");
		base.OnKill ();
	}
}
