using UnityEngine;
using System.Collections;

public class CrawlerController : MonoBehaviour {

	public AudioClip crawlerspawn;

	// Use this for initialization
	void Start () {
		SoundManager.instance.PlaySingle(crawlerspawn);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
