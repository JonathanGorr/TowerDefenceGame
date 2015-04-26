using UnityEngine;
using System.Collections;

public class CreeperController : MonoBehaviour {

	public float xspeed = 2;
	public float zspeed;
	private int[] randomZ = new int[] {-2, 2};

	private Rigidbody rigidbody;
	//public AudioClip creeperdmg1;
	//public AudioClip creeperdmg2;
	//public AudioClip creeperdeath1;
	public AudioClip creeperspawn1;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		//zspeed = 0f;
		SoundManager.instance.PlaySingle(creeperspawn1);
	}

	void Update () {
	}
	
	void FixedUpdate () {
		//rigidbody.velocity = new Vector3(xspeed, 0f, zspeed);
	}

	/*
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
}
