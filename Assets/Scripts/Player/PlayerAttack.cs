using UnityEngine;
using System.Collections;

public class PlayerAttack : Attack {

	public override void OnTriggerEnter (Collider other)
	{
		if (attacking) {

			//if the player hasnt dealt a single damage, can do damage
			if(!damageDealt)
			{
				//if the collider is an enemy, deal damage
				if (other.tag == "Enemy") {
					other.GetComponentInParent<EnemyHealth> ().TakeDamage (damage);
					damageDealt = true;
				}

				//delay
				StartCoroutine("Delay");
			}
		}
	}
}
