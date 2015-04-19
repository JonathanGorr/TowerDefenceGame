using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private PlayerController player;

	void Awake()
	{
		player = GetComponentInParent<PlayerController> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
			other.GetComponent<Health> ().TakeDamage(player.damage);
	}
}
