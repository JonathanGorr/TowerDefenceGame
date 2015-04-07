using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public int currency = 10;

	public Text soulText;
	public int soul;
	
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

	public void NextLevel()
	{
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
