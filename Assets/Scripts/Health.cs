using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	//values
	public int
		health,
		maxHealth;

	private float fadeOutTime = 2f;

	//bools
	public bool
		hurt,
		healing,
		dead,
		aggro,
		invincible;

	//components
	private Rigidbody rigidBody;
	[HideInInspector] public LevelManager manager;
	[HideInInspector] public StateMachine stateMachine;

	//transforms
	private Transform sprite;
	private Transform shadow;

	//audio
	public AudioClip[] hit;

	// Use this for initialization
	public virtual void Awake () {

		//import and find
		sprite = transform.Find ("Sprite");
		shadow = transform.Find ("Shadow");
		stateMachine = GetComponent<StateMachine> ();
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		rigidBody = GetComponent<Rigidbody> ();

		//set this to max on start
		health = maxHealth;
	}

	public virtual void Update()
	{
		//clamp this
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

			//play a random hit sound
			SoundManager.instance.PlaySingle (hit[Random.Range(0, hit.Length)]);
		}

		//if an enemy has no health left, drop blood and destroy object
		if (health <= 0)
		{
			dead = true;
			StartCoroutine(Fade( 0f, fadeOutTime));
		}

		if (stateMachine) {
			stateMachine.KnockBack ();
		}

		hurt = false;
	}
	
	//fades the sprite and shadow out over some time
	public virtual IEnumerator Fade(float aValue, float aTime)
	{
		float spriteAlpha = sprite.GetComponent<SpriteRenderer>().material.color.a;
		float meshAlpha = shadow.GetComponent<MeshRenderer> ().material.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {

			Color newSpriteColor = new Color (1, 1, 1, Mathf.Lerp (spriteAlpha, aValue, t));
			Color newMeshColor = new Color (1, 1, 1, Mathf.Lerp (meshAlpha, aValue, t));

			//assign new alpha values
			sprite.GetComponent<SpriteRenderer> ().material.color = newSpriteColor;
			shadow.GetComponent<MeshRenderer> ().material.color = newMeshColor;

			//Destroy gameobject if invisible
			if (t >= .99f) {
				OnKill ();
			}

			yield return null;
		}
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
