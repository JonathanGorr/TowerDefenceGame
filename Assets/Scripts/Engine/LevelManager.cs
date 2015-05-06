using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {

	//gameobjects
	private GameObject
		UI,
		pauseMenu, 
		blocksInstructions, 
		blockPanel, 
		instructionsPanel, 
		deathMenu,
		dayCanvas;

	//UI
	private Text dayText;
	[HideInInspector] public CanvasGroup canvas;

	//souls
	private Text soulText;
	[HideInInspector] public int souls = 10;

	//values
	private int day, mostDays, nextUnlock;
	private int unlockday3 = 3;
	private int unlockday6 = 6;
	private int unlockday9 = 9;
	private Text most, next;

	[HideInInspector] public bool inMenu, paused;

	void Awake () {

		//ui
		UI = GameObject.Find ("UI");
		pauseMenu = GameObject.Find ("PauseMenu");
		blocksInstructions = GameObject.Find ("BlocksInstructions");
		blockPanel = GameObject.Find ("blockPanel");
		instructionsPanel = GameObject.Find ("instructionsPanel");
		deathMenu = GameObject.Find ("DeathMenu");
		soulText = GameObject.Find("Souls").GetComponent<Text>();

		//days
		//current = GameObject.Find ("CurrentDay").GetComponent<Text>();
		most = GameObject.Find ("MostDays").GetComponent<Text>();
		next = GameObject.Find ("NextUnlock").GetComponent<Text>();
		mostDays = PlayerPrefs.GetInt("MostDays");
		nextUnlock = PlayerPrefs.GetInt("NextUnlock");

		//text
		most.text = "Most Days Survived: " + mostDays.ToString(" 00");
		next.text = "Next Unlock: " + nextUnlock.ToString(" 00");

		//target framerate
		Application.targetFrameRate = 60;

		//if the level name is a,b or c, inMenu is true
		inMenu = (Application.loadedLevelName == "Title" || 
		          Application.loadedLevelName == "ControlScreen" || 
		          Application.loadedLevelName == "KillScreen") ? true : false;

		//set these as inactive by default
		pauseMenu.SetActive (false);
		blocksInstructions.SetActive (false);
		blockPanel.SetActive (false);
		instructionsPanel.SetActive (false);
		deathMenu.SetActive (false);
		UI.SetActive((inMenu) ? false : true);

		//initialize soul system
		souls = 10;
		UpdateSoul();

		UnPause ();
	}

	void Update()
	{
		if(!inMenu)
		{
			//clamp souls between 0 and 999
			Mathf.Clamp(souls, 0, 999);

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

			if(Input.GetKeyDown(KeyCode.K))
				DeathScreen();
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

	/*
	public void UpdateDay(int newDayValue)
	{
		day += newDayValue;

		if (dayText)
			dayText.text = "DAY " + day;
		else
			print ("Day text cannot be found");

		if(day > mostDays){
			UpdateMostDays(day); // Replace the old most days if the current score is higher
		}
	}

	public void UpdateMostDays(int newMost){
		if (most)
			most.text = "Most Days Survived: " + newMost.ToString (" 00");
		else
			print ("There is no most");

		if(newMost > nextUnlock){
			UpdateNextUnlock(newMost);
		}
	}

	private void UpdateNextUnlock(int newUnlock){
		if (nextUnlock <= unlockday3)
			nextUnlock = unlockday3;
		else if (nextUnlock <= unlockday6)
			nextUnlock = unlockday6;
		else if (nextUnlock <= unlockday9)
			nextUnlock = unlockday9;

		if (next)
			next.text = "Next Unlock: " + newUnlock.ToString ("00");
		else
			print ("There is no next");
	}
	*/

	public void Pause()
	{
		pauseMenu.SetActive(true);
		blocksInstructions.SetActive(true);
		blockPanel.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void UnPause()
	{
		pauseMenu.SetActive(false);
		blocksInstructions.SetActive(false);
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

	public void DeathScreen()
	{
		Time.timeScale = 0;
		deathMenu.SetActive(true);
	}
	
	public void Restart()
	{
		PlayerPrefs.SetInt("MostDays", mostDays); // Save mostdays to PlayerPrefs
		deathMenu.SetActive(false);
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

