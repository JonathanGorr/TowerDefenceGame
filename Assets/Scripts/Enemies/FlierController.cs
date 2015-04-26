using UnityEngine;
using System.Collections;

public class FlierController : StateMachine {

	public AudioClip flierSpawn;

	// Use this for initialization
	public void Awake () {

		if(SoundManager.instance)
			SoundManager.instance.PlaySingle(flierSpawn);
	}
}
