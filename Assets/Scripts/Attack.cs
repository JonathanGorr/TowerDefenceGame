using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private PlayerController playerController;
	private TDEnemy enemyController;
	private GameObject player;
	private bool attacking;

	void Awake()
	{
		player = GameObject.Find ("Player");

		if(transform.parent.tag == "Enemy")
			enemyController = GetComponentInParent<TDEnemy> ();

		if(transform.parent.tag == "Player")
			playerController = GetComponentInParent<PlayerController> ();
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
			//if this script belongs to the player
			if(transform.parent.tag == "Player")
			{
				if (other.tag == "Enemy")
				{
					other.GetComponent<Health> ().TakeDamage(playerController.damage);
					//print("Enemy Hurt");
				}
			}
			else if (transform.parent.tag == "Enemy")
			{
				if (other.tag == "Player")
				{
					other.GetComponent<Health>().TakeDamage(enemyController.damage);
					//print("Player hurt");
				}
			}
		}
	}
}
