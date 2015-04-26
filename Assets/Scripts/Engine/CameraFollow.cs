using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	// Move the camera to follow a specified target. 
	// Includes a "dead zone" in which the target can move without causing the camera to follow.
	
	public GameObject target; // Add the GameObject you want to follow through the Inspector
	
	// Set limits to camera movement
	public bool limitCamera; 
	public float minX, maxX, minY, maxY;
	
	public float wiggleX, wiggleY; // "Dead zone" in which there is no camera movement
	public float smoothing = .5f; // Camera tracking smoothing. Higher=smoother+slower
	
	Vector3 position; // Variable to hold new position to move camera toward
	public Vector3 offset;
	Vector3 velocity = Vector3.zero; // Used by SmoothDamp...
	
	void Awake () {

		if(!target)
			target = GameObject.Find ("Player");

		// Set camera's starting x and y positions to target positions. keep current x position as-is
		position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
	}
	
	void Update () {
		
		// Check if the target exists before trying to follow it
		if (target){
			
			// Compare the absolute (non-negative) position of the camera and the target
			float xDifference = Mathf.Abs (target.transform.position.x - transform.position.x);
			float yDifference = Mathf.Abs (target.transform.position.y - transform.position.y);
			
			// Check if player's position to the camera has changed enough and set position to new target position
			if(xDifference >= wiggleX){
				position.x = target.transform.position.x;
			}
			if(yDifference >= wiggleY){
				position.y = target.transform.position.y;
			}
			
			// Limit the position using Mathf.Clamp
			if(limitCamera){
				position.x = Mathf.Clamp (position.x, minX, maxX);
				position.y = Mathf.Clamp (position.y, minY, maxY);
			}
			
			// SmoothDamp creates smooth camera movement
			transform.position = Vector3.SmoothDamp (transform.position, position + offset, ref velocity, smoothing);
		}
	}
}
