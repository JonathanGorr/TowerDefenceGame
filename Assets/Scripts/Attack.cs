using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private PlayerController playerController;
	private TDEnemy enemyController;
	private GameObject player;
	public bool attacking;

	[HideInInspector]
	public bool damageDealt;

	public int damage;
	public float delay = .05f;

	public AudioClip attack1;
	public AudioClip attack2;

	public virtual void Awake()
	{
		player = GameObject.Find ("Player");
	}

	public virtual void Attacking()
	{
		SoundManager.instance.RandomizeSfx (attack1, attack1);

		attacking = true;
	}

	public void NotAttacking()
	{
		attacking = false;
	}

	public IEnumerator Delay()
	{
		yield return new WaitForSeconds(delay);
		damageDealt = false;
	}

	public virtual void OnTriggerEnter(Collider other)
	{
	}
}
