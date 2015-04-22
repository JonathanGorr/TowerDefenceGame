using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectiveHealth : Health {

	private Slider healthBar;

	public void Start()
	{
		healthBar = GameObject.Find ("Heart").GetComponentInChildren<Slider>();
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public override void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.I))
			TakeDamage (10);

		healthBar.value = health;
		base.FixedUpdate ();
	}

	public override void TakeDamage (int value)
	{
		base.TakeDamage (value);
	}

	public override void OnKill ()
	{
		Application.LoadLevel ("KillScreen");
		base.OnKill ();
	}
}
