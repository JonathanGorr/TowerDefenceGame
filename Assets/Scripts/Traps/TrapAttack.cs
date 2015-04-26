using UnityEngine;
using System.Collections;

public class TrapAttack : Attack {

	public override void OnTriggerEnter (Collider other)
	{
		if (attacking) {
			if (transform.parent.tag == "Arrow") {
				if (other.tag == "Enemy") {
					//the object intersecting with the players hitbox should take damage
					other.GetComponentInParent<EnemyHealth> ().TakeDamage (damage);
				}
			} else if (transform.parent.tag == "Acid") {
				if (other.tag == "Enemy") {
					//the object intersecting with the players hitbox should take damage
					other.GetComponentInParent<EnemyHealth> ().TakeDamage (damage);
				}
			}
		}
	}
}
