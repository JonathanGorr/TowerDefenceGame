using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	//objects
	public GameObject soul;

	//values
	public float 
		verticalForce = 300f, 
		horizontalForce = 50f,
		spawnRate = 1f;

	public void Spawn(int number)
	{
		StartCoroutine("Drop", number);
	}

	IEnumerator Drop(int number)
	{
		//spawns a number of souls
		for(int i = 0; i < number; i++)
		{
			GameObject obj = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;
			
			//throw the soul vertically
			obj.GetComponent<Rigidbody>().AddForce(Vector3.up * verticalForce);
			
			//pick 0 or 1
			bool direction = RandomBool();
			
			//throw left or right depending
			if(direction)
				obj.GetComponent<Rigidbody>().AddForce(Vector3.left * horizontalForce);
			else
				obj.GetComponent<Rigidbody>().AddForce(Vector3.right * horizontalForce);
			
			yield return new WaitForSeconds(spawnRate);
		}
	}

	public bool RandomBool()
	{
		if(Random.value >= 0.5)
		{
			return true;
		}
		return false;
	}
}
