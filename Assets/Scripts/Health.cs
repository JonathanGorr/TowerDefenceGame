﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public int 
		health,
		maxHealth;

	public bool
		playerHurt,
		enemyHurt,
		healing,
		dead,
		aggro,
		invincible;

	[HideInInspector]
	public int playerDamage, enemyDamage;
	
	private HitPoints _hitPoints;
	private Slider healthBar;
	//private PlayerPreferences _prefs;

	// Use this for initialization
	void Awake () {
		health = maxHealth;

		//healthbar init
		healthBar = GameObject.Find ("Health").GetComponent<Slider>();
		healthBar.minValue = 0;
		healthBar.maxValue = maxHealth;
		healthBar.value = health;

		_hitPoints = GetComponent<HitPoints>();
	}

	void FixedUpdate()
	{
		healing = false;
		//clamp health
		Mathf.Clamp (health, 0, maxHealth);
	}

	public void PlayerTakeDamage(int value)
	{
		if(!invincible)
		{
			//entity is hurt(player), trigger a knockkback animation
			playerHurt = true;

			playerDamage = value;
			_hitPoints.TakeDamage(playerDamage);

			//when a weapon collides, subtract health by the passes int(damage)
			health -= playerDamage;
		}

		//if an enemy has no health left, drop blood and destroy object
		if (health <= 0)
		{
			dead = true;
			//OnKill();
		}
	}

	public void EnemyTakeDamage(int value)
	{
		//when a weapon collides, subtract health by the passes int(value)
		health -= value;
		//entity is hurt(player), trigger a knockkback animation
		enemyHurt = true;
		//assign the hitpoint to the damage done
		enemyDamage = value;
		//instantiate hitpoint
		_hitPoints.TakeDamage(enemyDamage);
		//make enemy aware of player and chase if damaged
		aggro = true;

		//if an enemy has no health left, drop blood and destroy object
		if (health <= 0)
		{
			//OnKill();
		}
	}

	public void Heal(int heal)
	{
		health += heal;
		healing = true;
		if(_hitPoints != null)
			_hitPoints.Heal(heal);
	}

	public void Invincible()
	{
		invincible = !invincible;
	}

	/*
	public void OnKill()
	{
		if(_explode != null)
		{
			//enemyHurt = false;
			_explode.OnExplode();

			if(gameObject.tag == "Enemy")
			{
				enemyHurt = false;
				Destroy(gameObject);
			}
			else if(gameObject.tag == "Player")
			{
				playerHurt = false;
			}
		}
		else
			print("enemy was already destroyed");
	}
	*/
}
