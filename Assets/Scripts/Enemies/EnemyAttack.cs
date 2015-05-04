using UnityEngine;
using System.Collections;

public class EnemyAttack : Attack {

	public override void OnTriggerEnter (Collider other)
	{
		if (attacking) {

			//if the player hasnt dealt a single damage, can do damage
			if(!damageDealt)
			{
				//if the collider is an enemy...
				if (other.transform.parent.tag == "Player") {

					print("damage done to player");

					other.transform.parent.GetComponentInChildren<PlayerHealth>().TakeDamage (damage);
					other.transform.parent.GetComponentInChildren<PlayerController>().playerHit = true;

					//damage is dealt
					damageDealt = true;
					//delay
					StartCoroutine("Delay");
				}
				//if the object is the objective....
				else if(other.transform.parent.tag == "Heart") {

					print("Objective hit by enemy");

					other.GetComponentInParent<ObjectiveHealth> ().TakeDamage (damage);
					
					//damage is dealt
					damageDealt = true;
					//delay
					StartCoroutine("Delay");
				}
			}
		}
	}
}
