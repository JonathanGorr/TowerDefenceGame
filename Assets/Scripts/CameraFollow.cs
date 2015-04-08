using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour { 
	public float interpVelocity;
	public float minDistance;
	public float followDistance, speed = 5f;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;

	void Start () {
		targetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (target)
        {
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			Vector3 targetDirection = (target.transform.position - posNoZ); 
			interpVelocity = targetDirection.magnitude * speed;
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
			targetPos += offset;
	        transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);
		}
	}
}
