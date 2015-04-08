using UnityEngine;
using System.Collections;

public class CreeperController : MonoBehaviour {

	public float xspeed = 2;
	public float zspeed;
	public int health = 4;

	private int[] randomZ = new int[] {-2, 2};

	private Rigidbody rigidbody;
	private GameObject soul;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		soul = GameObject.Find("Soul");
		zspeed = 0f;
	}

	void Update () {
		CheckDeath ();
	}
	
	void FixedUpdate () {
		rigidbody.velocity = new Vector3(xspeed, 0f, zspeed);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "FrontWall")
		{
			zspeed = 2f;
			xspeed = 2f;
		}
		else if (col.gameObject.tag == "BackWall")
		{
			zspeed = -2f;
			xspeed = 2f;
		}
		else if (col.gameObject.tag == "Cube")
		{
			zspeed = randomZ[Random.Range(0, randomZ.Length)];
			xspeed = 1f;
		}
	}

	public void TakeDamage (int dmg)
	{
		//lose health based on dmg taken
		health -= dmg;

		//SoundManager.instance.RandomizeSfx (creeperdmg1, creeperdmg2);

		CheckDeath ();
	}

	private void CheckDeath ()
	{
		//Check if food point total is less than or equal to zero.
		if (health <= 0)
		{
			//SoundManager.instance.RandomizeSfx (creeperdeath1, creeperdeath2);
			GameObject obj = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;

			Destroy(gameObject);
		}
	}

}
