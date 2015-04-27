using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public int
		health,
		maxHealth;

	public bool
		hurt,
		healing,
		dead,
		aggro,
		invincible;
	
	private Rigidbody rigidBody;
	public int force;
	public LevelManager manager;

	public AudioClip hit;
	public AudioClip hit2;
	public AudioClip hit3;

	// Use this for initialization
	public virtual void Awake () {
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		rigidBody = GetComponent<Rigidbody> ();

		//set this to max on start
		health = maxHealth;
	}

	public virtual void FixedUpdate()
	{
		//clamp health
		//Mathf.Clamp (health, 0, maxHealth);
	}

	public virtual void TakeDamage(int value)
	{
		//if not invincible
		if(!invincible)
		{
			//entity is hurt(player), trigger a knockkback animation
			hurt = true;
			//KnockBack();

			//when a weapon collides, subtract health by the passes int(damage)
			health -= value;
			SoundManager.instance.PlaySingle (hit);
		}

		//if an enemy has no health left, drop blood and destroy object
		if (health <= 0)
		{
			dead = true;
			OnKill();
		}

		if (rigidBody) {
			//apply a force on hit
			KnockBack ();
		}
		hurt = false;

		//print ("hit for " + value + " damage");
	}

	public virtual void KnockBack()
	{
		rigidBody.AddForce (Vector3.left * force);
		rigidBody.AddForce (Vector3.up * force);
	}

	public virtual void Heal(int heal)
	{
		health += heal;
		healing = true;
	}

	public virtual void Invincible()
	{
		invincible = !invincible;
	}
	
	public virtual void OnKill()
	{
		Destroy (gameObject);
	}
}
