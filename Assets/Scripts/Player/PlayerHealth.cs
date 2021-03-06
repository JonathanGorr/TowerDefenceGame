﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Health {

	private Slider healthBar;
	private PlayerController controller;

	public override void Awake ()
	{
		base.Awake ();

		controller = GetComponent<PlayerController> ();

		health = 10;
		maxHealth = 10;

		healthBar = GameObject.Find ("Health").GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public override void TakeDamage(int value)
	{
		base.TakeDamage (value);
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

	public override void OnKill()
	{
		base.manager.DeathScreen();
		Destroy (gameObject);
	}
}
