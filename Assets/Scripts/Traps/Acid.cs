using UnityEngine;
using System.Collections;

public class Acid : MonoBehaviour {

	public GameObject deathInstance = null;
	private Vector2 deathInstanceOffset = new Vector2(0,0);
	
	void OnTriggerEnter (Collider col) 
	{
		if(col.tag == "Enemy" || col.tag == "Player" || col.tag == "Ground")
		{
			OnKill();
		}
	}

	void OnKill()
	{
		if (deathInstance) 
		{
			var pos = gameObject.transform.position;
			GameObject clone = Instantiate (deathInstance, new
			Vector3(pos.x + deathInstanceOffset.x, pos.y + deathInstanceOffset.y,
			pos.z), Quaternion.identity) as GameObject;
		}
			Destroy(gameObject);
	}
}
