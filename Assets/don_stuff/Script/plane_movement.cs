using UnityEngine;
using System.Collections;

public class plane_movement : MonoBehaviour {
	//you will only need to change this.
	public float xspeed = .02f;
	public float yspeed = .02f;

	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode downKey = KeyCode.S;
	public KeyCode upKey = KeyCode.W;
	// Update is called once per frame
	void Update () {
		//vector control 3 point the (x, y ,and z)
		//postion is the name of our variable
		// the = sign means setting  the value on the left to the thing on the right.

		//movement
		Vector3 position = transform.position; 

		if(Input.GetKey(leftKey) ){
			position.x -= xspeed;
		}
		if(Input.GetKey(rightKey) ){
			position.x += xspeed;
		}
		if(Input.GetKey(downKey) ){
			position.y -= yspeed;
		}
		if(Input.GetKey(upKey) ){
			position.y += yspeed;
		}
		transform.position = position;

	}
		


}
