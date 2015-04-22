using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Health {
	
	private Slider healthBar;

	public override void Awake ()
	{
		base.Awake ();

		health = 20;
		maxHealth = 20;

		healthBar = GameObject.Find ("Health").GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public override void FixedUpdate ()
	{
		healthBar.value = health;
		base.FixedUpdate ();
	}
}
