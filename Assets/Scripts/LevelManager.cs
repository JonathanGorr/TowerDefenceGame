﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {

	private GameObject UI, pauseMenu;
	private Text dayText;
	private GameObject moonParent;
	private CanvasGroup canvas;

	private Text soulText;
	public int souls = 10;

	public float dayCycleInMinutes = .1f;
	public const float SECOND = 1;
	public const float MINUTE = 60 * SECOND;
	public const float HOUR = 60 * MINUTE;
	public const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;

	private float degreeRotation;

	public float daylength = 3f;
	public float nightlength = 3f;
	private float spinRate;
	private int day = 0;

	private GameObject dayCanvas;
	public Color nightColor;
	public Color dayColor;
	private Color currentColor;
	private Color nextColor;
	public float skyTransitionSpeed = 0.06f;
	public float dayCanvasTransitionSpeed = 0.1f;

	[HideInInspector]
	public bool inMenu, paused;

	public AudioClip newDay;

	public AudioSource nightTrack1;
	
	void Awake () {

		UI = GameObject.Find ("UI");

		//day/night
		dayCanvas = GameObject.Find ("DayCanvas");

		if (dayCanvas) {
			canvas = dayCanvas.GetComponent<CanvasGroup> ();
			dayText = dayCanvas.transform.Find ("DayText").GetComponent<Text> ();
			moonParent = GameObject.Find("MoonParent");
		}

		//ui
		pauseMenu = GameObject.Find ("PauseMenu");
		soulText = GameObject.Find("Souls").GetComponent<Text>();

		Application.targetFrameRate = 60;

		inMenu = (Application.loadedLevelName == "Title" || 
		          Application.loadedLevelName == "ControlScreen" || 
		          Application.loadedLevelName == "KillScreen") ? true : false;

		pauseMenu.SetActive (false);
		UI.SetActive((inMenu) ? false : true);

		//initialize soul system
		souls = 10;
		UpdateSoul();

		//set constants for spinning moon/day cycle
        spinRate = daylength + nightlength;
        //dayCycleInMinutes = .1f;
		degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);
		StartCoroutine (DayCycle ());

		currentColor = dayColor;
		Camera.main.backgroundColor = currentColor;
		RenderSettings.ambientLight = currentColor;

		if(dayCanvas)
			canvas.alpha = 0.0f;
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
		}

		if(moonParent)
			moonParent.transform.Rotate(new Vector3(0, 0, -degreeRotation) * Time.deltaTime);

		currentColor = Color.Lerp(currentColor, nextColor, skyTransitionSpeed);
		Camera.main.backgroundColor = currentColor;
		RenderSettings.ambientLight = currentColor;
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

	public void UpdateDay(int newDayValue)
	{
		day += newDayValue;

		if (dayText)
			dayText.text = "DAY " + day;
		else
			print ("Day text cannot be found");
	}

	
	IEnumerator DayCycle ()
    {
        while (true)
        {
        	UpdateDay(1);

            yield return new WaitForSeconds (daylength);
            //day stuff here
           		nextColor = dayColor;
				canvas.alpha = 1f;
				SoundManager.instance.PlaySingle (newDay);
				nightTrack1.Stop();
				yield return new WaitForSeconds (3);
				canvas.alpha = 0f;
            yield return new WaitForSeconds (nightlength);
            //night stuff here
            	nightTrack1.Play();
            	nextColor = nightColor;
        }
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

