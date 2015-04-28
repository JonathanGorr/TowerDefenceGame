using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour {

	private LevelManager manager;

	void Start () {
		manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Soul")
		{
			//col.gameObject.GetComponent<SoulToPlayer>().active = true;
		}
	}
}
