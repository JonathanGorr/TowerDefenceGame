using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	[HideInInspector]
	public bool action, moving, jump;

	[HideInInspector]
	public Vector3 axis;

	void Update()
	{
		jump = (Input.GetButton ("Jump"));
		action = Input.GetMouseButtonDown (0);
		moving = (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0 || Mathf.Abs (Input.GetAxis ("Vertical")) > 0) ? true : false;
		axis = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
	}
}
