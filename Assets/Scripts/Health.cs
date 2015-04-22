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

	private Slider healthBar;
	public GameObject soul;
	private Rigidbody rigidBody;
	public int force;

	// Use this for initialization
	public virtual void Awake () {

		rigidBody = GetComponent<Rigidbody> ();

		healthBar = GameObject.Find ("Health").GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;

		//set this to max on start
		health = maxHealth;
	}

	public virtual void FixedUpdate()
	{
		healthBar.value = health;

		//clamp health
		Mathf.Clamp (health, 0, maxHealth);
	}

	public virtual void TakeDamage(int value)
	{
		//if not invincible
		if(!invincible)
		{
			//entity is hurt(player), trigger a knockkback animation
			hurt = true;

			//when a weapon collides, subtract health by the passes int(damage)
			health -= value;
		}

		//if an enemy has no health left, drop blood and destroy object
		if (health <= 0)
		{
			dead = true;
			OnKill();
		}

		//apply a force on hit
		KnockBack ();

		print ("hit for " + value + " damage");
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
