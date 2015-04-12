using UnityEngine;
using System.Collections;

public class CreeperController : MonoBehaviour {

	public float xspeed = 2;
	public float zspeed;

	private int[] randomZ = new int[] {-2, 2};

	private Rigidbody rigidbody;
	private GameObject soul;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		soul = GameObject.Find("Soul");
		zspeed = 0f;
	}

	void Update () {
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

	/*void OnTriggerStay (Collider col2)
	{
		/*if (col2.gameObject.tag == "FrontWall")
		{
			zspeed = 2f;
			xspeed = 2f;
		}
		else if (col2.gameObject.tag == "BackWall")
		{
			zspeed = -2f;
			xspeed = 2f;
		}
		if (col2.gameObject.tag == "Cube")
		{
			Invoke("waitblock", 2);
			
		}
	}

	public void waitblock ()
	{
		zspeed = randomZ[Random.Range(0, randomZ.Length)];
		xspeed = 3f;
	}*/




	public void TakeDamage ()
	{
		//SoundManager.instance.RandomizeSfx (creeperdmg1, creeperdmg2);
	}

	private void Death ()
	{
		//SoundManager.instance.RandomizeSfx (creeperdeath1, creeperdeath2);
	}

}
