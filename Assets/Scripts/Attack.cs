using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private PlayerController playerController;
	private TDEnemy enemyController;
	private GameObject player;
	private bool attacking;
	public int damage;

	void Awake()
	{
		player = GameObject.Find ("Player");
	}

	public void Attacking()
	{
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

			/*
			//if the gameobject is an enemy, and is colliding with the player, attack
			else if (transform.parent.tag == "Enemy")
			{
				if (other.transform.parent.tag == "Player")
				{
					other.GetComponent<Health>().TakeDamage(playerController.damage);
					print("Player hurt");
				}
			}
			*/
		}
	}
}
