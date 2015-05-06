using UnityEngine;
using System.Collections;

public class acidPop : MonoBehaviour {

	public float duration = .3f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
