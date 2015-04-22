using UnityEngine;
using System.Collections;

public class fliercontroller : MonoBehaviour {

	public AudioClip flierspawn;

	// Use this for initialization
	void Start () {
		SoundManager.instance.PlaySingle(flierspawn);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
