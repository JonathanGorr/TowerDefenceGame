using UnityEngine;
using System.Collections;

public class KeyboardRigidbody : MonoBehaviour {

	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;

	public float xspeed = 10f;
	public float zspeed = 5f;


	void FixedUpdate () {
		// Stop movement when there is no input
		Vector3 velocity = new Vector3(0,0,0);

		// x movement with velocity
		if(Input.GetKey(leftKey)){
			velocity.x = -xspeed;
		}
		else if(Input.GetKey(rightKey)){
			velocity.x = xspeed;
		}

		// y movement with velocity
		if(Input.GetKey(upKey)){
			velocity.z = zspeed;
		}
		else if(Input.GetKey(downKey)){
			velocity.z = -zspeed;
		}

		GetComponent<Rigidbody>().velocity = velocity;

	}
}
