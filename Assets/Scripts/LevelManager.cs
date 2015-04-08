using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public int souls = 10;

	private GameObject UI, pauseMenu;

	private Text soulText;

	[HideInInspector]
	public bool inMenu, paused;
	
	void Awake () {
		UI = GameObject.Find ("UI");
		pauseMenu = GameObject.Find ("PauseMenu");
		soulText = GameObject.Find("Souls").GetComponent<Text>();
		Application.targetFrameRate = 60;
		inMenu = (Application.loadedLevelName == "Title" || Application.loadedLevelName == "ControlScreen") ? true : false;

		pauseMenu.SetActive (false);
		UI.SetActive((inMenu) ? false : true);

		souls = 10;
		UpdateSoul();
	}

	void Update()
	{
		if(!inMenu)
		{
			//pause
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				//toggle
				paused = !paused;
				
				if(paused)
				{
					Pause();
				}
				else
				{
					Resume();
				}
			}

			if(Input.GetKeyDown(KeyCode.R))
				Restart();
		}
	}
	
	public void AddSoul (int value)
	{
		souls += value;
		UpdateSoul();
	}
	public void SubtractSoul (int value)
	{
		souls -= value;
		UpdateSoul();
	}
	
	void UpdateSoul()
	{
		if (soulText)
			soulText.text = "Souls: " + souls;
		else
			print ("Soul text cannot be found");
	}

	public void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void UnPause()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
	
	public void Resume()
	{
		UnPause();
	}
	
	public void GoToMenu()
	{
		UnPause();
		Application.LoadLevel("Title");
	}
	
	public void Restart()
	{
		UnPause();
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void NextLevel()
	{
		UnPause();
		
		//if there is another level, load it, otherwise go to menu or, ideally, game over screen
		if(Application.CanStreamedLevelBeLoaded(Application.loadedLevel + 1))
			Application.LoadLevel(Application.loadedLevel + 1);
		else
			Application.LoadLevel("Title");
	}
	
	public void Quit()
	{
		UnPause();
		Application.Quit();
	}
}

