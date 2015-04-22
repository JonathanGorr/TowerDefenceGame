using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Health {

	private Slider healthBar;

	public override void Awake ()
	{
		healthBar = GameObject.Find ("Health").GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;

		base.Awake ();
	}

	public override void FixedUpdate ()
	{
		healthBar.value = health;
		base.FixedUpdate ();
	}
}
