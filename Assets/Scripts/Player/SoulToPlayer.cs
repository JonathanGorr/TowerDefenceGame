using UnityEngine;
using System.Collections;

public class SoulToPlayer : MonoBehaviour {

	private GameObject player;
	private Vector3 playerPos;
	private LevelManager manager;

	public float speed = .1f;
	public float accel = 10f;
	private bool active = false;

	public AudioClip collect;

	void Start () {
		player = GameObject.Find("Player");
		manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		active = false;
	}

	void Update() {
		playerPos = player.transform.position;
	}

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Attractor")
		{
			active = true;
		}

		if(col.tag == "Player")
		{
			SoundManager.instance.PlaySingle (collect);
			manager.AddSoul(1);
			Destroy(gameObject);
		}
	}

	void FixedUpdate () {
		if (active == true) 
		{
			transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
			speed = speed + (accel * Time.deltaTime);
		}

	} 
}
