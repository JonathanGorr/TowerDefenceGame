using UnityEngine;
using System.Collections;

public class FlierController : StateMachine {

	public AudioClip flierSpawn;

	// Use this for initialization
	public void Start () {

		if(SoundManager.instance)
			SoundManager.instance.PlaySingle(flierSpawn);
	}
}
