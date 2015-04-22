using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectiveHealth : Health {

	private Slider healthBar;

	public override void Awake ()
	{
		healthBar = GameObject.Find ("Heart").GetComponentInChildren<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;

		print (health);

		base.Awake ();
	}

	public override void FixedUpdate ()
	{
		healthBar.value = health;
		base.FixedUpdate ();
	}
}
