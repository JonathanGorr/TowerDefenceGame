using UnityEngine;
using System.Collections;

public class SoulToPlayer : MonoBehaviour {

	private Vector3 player;
	public float speed = 8f;
	private bool idle = false;

	void Start () {
		player = GameObject.Find("Player").transform.position;
		idle = false;
	}

	public void Activate() {
		idle = true;
	}

	void FixedUpdate () {
		if (idle == true) 
		{
			transform.position = Vector3.MoveTowards(transform.position, player, speed * Time.deltaTime);
		}
	} 
	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
