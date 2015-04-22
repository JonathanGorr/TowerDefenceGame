﻿using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private PlayerController playerController;
	private TDEnemy enemyController;
	private GameObject player;
	public bool attacking;
	public int damage;

	public AudioClip attack1;
	public AudioClip attack2;

	void Awake()
	{
		player = GameObject.Find ("Player");
	}

	public void Attacking()
	{
		SoundManager.instance.RandomizeSfx (attack1, attack1);
		attacking = true;
	}

	public void NotAttacking()
	{
		attacking = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(attacking)
		{
			//if this script is on the player, and is inside the enemy
			if(transform.parent.tag == "Player")
			{
				if (other.tag == "Enemy")
				{
					//the object intersecting with the players hitbox should take damage
					other.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
					//print("Enemy Hurt");
				}
			}
			else if(transform.parent.tag == "Arrow")
			{
				if (other.tag == "Enemy")
				{
					//the object intersecting with the players hitbox should take damage
					other.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
					print("Enemy Hurt");
				}
			}
			else if(transform.parent.tag == "Acid")
			{
				if (other.tag == "Enemy")
				{
					//the object intersecting with the players hitbox should take damage
					other.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
					//print("Enemy Hurt");
				}
			}

			//if the gameobject is an enemy, and is colliding with the player, attack
			else if (transform.parent.tag == "Enemy")
			{
				if (other.transform.parent.tag == "Player")
				{
					other.GetComponent<PlayerHealth>().TakeDamage(damage);
					print("Player hurt");
				}

				else if (other.transform.parent.tag == "Heart")
				{
					other.GetComponent<ObjectiveHealth>().TakeDamage(damage);
					print("Player hurt");
				}
			}
		}
	}
}
