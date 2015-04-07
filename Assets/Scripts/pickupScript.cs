using UnityEngine;
using System.Collections;

public class pickupScript : MonoBehaviour {

	public GameManager soul;	

	// Use this for initialization
	void Start () {
		soul = GameObject.Find("LevelManager").GetComponent<GameManager>();
	}

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Soul")
		{
			soul.AddSoul(1);
			Destroy(col.gameObject);
		}
	}
}
