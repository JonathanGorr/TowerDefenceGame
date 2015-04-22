using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Health {
	
	private Slider healthBar;
	//private LevelManager manager;

	public override void Awake ()
	{
		base.Awake ();

		//manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();

		health = 20;
		maxHealth = 20;

		healthBar = GameObject.Find ("Health").GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public override void FixedUpdate ()
	{
		healthBar.value = health;
		base.FixedUpdate ();
	}

	public override void OnKill()
	{
		base.manager.GoToMenu();
		Destroy (gameObject);
		//base.OnKill();
	}
}
