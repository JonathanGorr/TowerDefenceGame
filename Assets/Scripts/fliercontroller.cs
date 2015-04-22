using UnityEngine;
using System.Collections;

public class FlierController : SmasherController {

	public AudioClip flierSpawn;

	// Use this for initialization
	public override void Awake () {

		if(SoundManager.instance)
			SoundManager.instance.PlaySingle(flierSpawn);

		base.Awake ();
	}
}
