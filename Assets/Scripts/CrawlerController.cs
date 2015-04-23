using UnityEngine;
using System.Collections;

public class CrawlerController : StateMachine {

	public AudioClip crawlerSpawn;

	// Use this for initialization
	void Start () {
		SoundManager.instance.PlaySingle(crawlerSpawn);
	}
}
