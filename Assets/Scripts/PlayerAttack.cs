using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private PlayerController player;
	private bool attacking;

	void Awake()
	{
		player = GetComponentInParent<PlayerController> ();
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
			if (other.tag == "Enemy")
			{
				other.GetComponent<Health> ().TakeDamage(player.damage);
				print("damageDealt");
			}
		}
	}
}
