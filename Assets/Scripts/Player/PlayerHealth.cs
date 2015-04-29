﻿using UnityEngine;
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

	public override void TakeDamage(int value)
	{

		base.TakeDamage (value);
	}

	public override void Update ()
	{
		healthBar.value = health;
		base.Update ();
	}

	public override void OnKill()
	{
		//base.manager.GoToMenu();
		Destroy (gameObject);
	}

	public void KnockBack()
	{

	}
}
