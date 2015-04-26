using UnityEngine;
using System.Collections;

public class EnemyAttack : Attack {

	public override void OnTriggerEnter (Collider other)
	{
		if (attacking) {

			//if the player hasnt dealt a single damage, can do damage
			if(!damageDealt)
			{
				// if the collider is the player, deal damage
				if (other.transform.parent.tag == "Player") {

					other.GetComponentInParent<PlayerHealth> ().TakeDamage (damage);

					//damage has been dealt
					damageDealt = true;
				} 
				// if the collider is the objective, deal damage
				else if (other.transform.parent.tag == "Heart") 
				{
					other.GetComponent<ObjectiveHealth> ().TakeDamage (damage);

					//damage has been dealt
					damageDealt = true;
				}

				//delay
				StartCoroutine("Delay");
			}
		}
	}
}
