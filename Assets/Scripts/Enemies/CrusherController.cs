using UnityEngine;
using System.Collections;

public class CrusherController : MonoBehaviour {

	public AudioClip[] spawnSounds;
	public float xspeed = 1;
	private Rigidbody rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();

		xspeed = 1;
		SoundManager.instance.PlaySingle(spawnSounds[Random.Range(0, spawnSounds.Length)]);
	}


	void FixedUpdate () {
		rigidbody.velocity = new Vector3(xspeed, 0f, 0f);
	}
}

