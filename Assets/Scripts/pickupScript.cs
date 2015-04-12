using UnityEngine;
using System.Collections;

public class pickupScript : MonoBehaviour {

	private LevelManager manager;
	//public SoulToPlayer soul;	

	void Start () {
		manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		//soul = GameObject.Find("Soul").GetComponent<SoulToPlayer>();
	}

	private void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Soul")
		{
			manager.AddSoul(1);
			col.gameObject.GetComponent<SoulToPlayer>().Activate();
			//col.gameObject.GetComponent<SoulToPlayer>().Activate();
			//Destroy(col.gameObject);
		}
	}
}
