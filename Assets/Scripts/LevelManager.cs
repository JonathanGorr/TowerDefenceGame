using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public int currency = 10;

	private Text soulText;
	public int soul = 0;
	
	void Awake () {
		soulText = GameObject.Find("Currency").GetComponent<Text>();
		UpdateSoul();
	}
	
	public void AddSoul (int newSoulValue)
	{
		soul += newSoulValue;
		UpdateSoul();
	}
	
	void UpdateSoul()
	{
		if (soulText)
			soulText.text = "Souls: " + soul;
		else
			print ("Soul text cannot be found");
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
