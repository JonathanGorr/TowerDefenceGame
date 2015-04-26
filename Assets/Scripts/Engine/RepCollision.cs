using UnityEngine;
using System.Collections;

public class RepCollision : MonoBehaviour {

	[HideInInspector]
	public bool isColWithPlayer, isInAnotherCube;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			print("in the player");
			isColWithPlayer = true;
		}

		if(other.gameObject.tag == "Cube")
		{
			print("in another cube");
			isInAnotherCube = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			isColWithPlayer = false;
		}

		if(other.gameObject.tag == "Cube")
		{
			isInAnotherCube = false;
		}
	}
}
