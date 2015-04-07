using UnityEngine;
using System.Collections;

public class pickupScript : MonoBehaviour {

	public LevelManager manager;	

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Soul")
		{
			manager.AddSoul(1);
			Destroy(col.gameObject);
		}
	}
}
