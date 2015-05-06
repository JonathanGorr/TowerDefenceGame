using UnityEngine;
using System.Collections;

public class ProjectileAttack : Attack {

	public override void OnTriggerEnter (Collider other)
	{
		if (attacking) {

			//if the player hasnt dealt a single damage, can do damage
			if(!damageDealt)
			{
				//if the collider is an enemy...
				if (other.tag == "Enemy") {
					other.GetComponentInParent<EnemyHealth> ().TakeDamage (damage);
					//damage is dealt
					damageDealt = true;
					//delay
					StartCoroutine("Delay");
				}
			}
		}
	}
}
