using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	//[HideInInspector]
	public Text soulText;
	public int soul;

	// Use this for initialization
	void Start () {
		soul = 0;
		UpdateSoul ();
		soulText = GameObject.Find("SoulText").GetComponent<Text>();
	}

	public void AddSoul (int newSoulValue)
	{
		soul += newSoulValue;
		UpdateSoul ();
	}

	void UpdateSoul() 
	{
		soulText.text = "Souls: " + soul;
	}

	public IEnumerator ReloadLevel()
	{
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Pause()
	{
	}

	public void Resume()
	{
	}

	public void Quit()
	{
	}

	public void ReturnToMenu()
	{
	}
}
