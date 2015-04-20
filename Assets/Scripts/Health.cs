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

	// Use this for initialization
	void Awake () {
		healthBar = GameObject.Find ("Health").GetComponent<Slider>();
		health = maxHealth;

		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	void FixedUpdate()
	{
		healthBar.value = health;
		//clamp health
		Mathf.Clamp (health, 0, maxHealth);
	}

	public void TakeDamage(int value)
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
	}

	public void Heal(int heal)
	{
		health += heal;
		healing = true;
	}

	public void Invincible()
	{
		invincible = !invincible;
	}
	
	public void OnKill()
	{
		Destroy (gameObject);
	}
}
