using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {

	private GameObject UI, pauseMenu, blocksInstructions, blockPanel, instructionsPanel, deathMenu;
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
	//private float spinRate;
	private int day, mostDays, nextUnlock;
	private int unlockday3 = 3;
	private int unlockday6 = 6;
	private int unlockday9 = 9;
	private Text most, next;

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

		dayCanvas = GameObject.Find ("DayCanvas");

		if (dayCanvas) {
			canvas = dayCanvas.GetComponent<CanvasGroup> ();
			dayText = dayCanvas.transform.Find ("DayText").GetComponent<Text> ();
			moonParent = GameObject.Find("MoonParent");
		}

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

		//set constants for spinning moon/day cycle
        //spinRate = daylength + nightlength;
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

		if(day > mostDays){
			UpdateMostDays(day); // Replace the old most days if the current score is higher
		}
	}

	public void UpdateMostDays(int newMost){
		most.text = "Most Days Survived: " + newMost.ToString("00");

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

		next.text = "Next Unlock: " + newUnlock.ToString("00");
	}


	IEnumerator DayCycle ()
    {
		yield return null;
		/*
		//TODO:
		//Upon Dawn, find all enemies then kill them
		
		//get all the enemies alive
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		//kill them all
		foreach(GameObject enemy in enemies)
		{
			enemy.GetComponent<EnemyHealth>().OnKill();
		}

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
        */
    }

	public void Pause()
	{
		pauseMenu.SetActive(true);
		blocksInstructions.SetActive(true);
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
		deathMenu.SetActive(true);
		Time.timeScale = 0;
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

