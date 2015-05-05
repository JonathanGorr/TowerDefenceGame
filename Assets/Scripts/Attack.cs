using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	//components
	private PlayerController playerController;
	private TDEnemy enemyController;
	private GameObject player;
	private AudioSource source;

	//bools
	public bool attacking;
	[HideInInspector]
	public bool damageDealt;

	//values
	public int damage;
	public float delay = .05f;

	//sounds
	public AudioClip[] attacks;

	public virtual void Awake()
	{
		source = GetComponent<AudioSource> ();
		player = GameObject.Find ("Player");
	}

	public virtual void Attacking()
	{
		source.PlayOneShot(attacks[Random.Range(0, attacks.Length)]);
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

	public virtual void OnTriggerStay(Collider other)
	{
	}
}
