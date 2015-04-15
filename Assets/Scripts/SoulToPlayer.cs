using UnityEngine;
using System.Collections;

public class SoulToPlayer : MonoBehaviour {

	private GameObject player;
	private Vector3 playerPos;
	public float speed = 8f;

	[HideInInspector]
	public bool active = false;

	void Start () {
		player = GameObject.Find("Player");
		active = false;
	}

	void Update () {

		playerPos = player.transform.position;

		print (active);

		if (active)
		{
			transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
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
