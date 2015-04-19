using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	//[HideInInspector]

	private GameObject UI, pauseMenu;
	private Text dayText;
	private GameObject moonParent;

	private Text soulText;
	public int souls = 10;

	public float dayCycleInMinutes = .5f;
	public const float SECOND = 1;
	public const float MINUTE = 60 * SECOND;
	public const float HOUR = 60 * MINUTE;
	public const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;

	private float degreeRotation;

	public float daylength = 4f;
	public float nightlength = 6f;
	private float spinRate;
	public int day = 0;

	//[HideInInspector]
	public bool inMenu, paused;
	
	void Awake () {
		UI = GameObject.Find ("UI");
		pauseMenu = GameObject.Find ("PauseMenu");
		soulText = GameObject.Find("Souls").GetComponent<Text>();
		dayText = GameObject.Find("DayText").GetComponent<Text>();
		moonParent = GameObject.Find("MoonParent");


		Application.targetFrameRate = 60;
		inMenu = (Application.loadedLevelName == "Title" || Application.loadedLevelName == "ControlScreen") ? true : false;

		pauseMenu.SetActive (false);
		UI.SetActive((inMenu) ? false : true);

		souls = 10;

        spinRate = daylength + nightlength;
        //dayCycleInMinutes = .1f;
		degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);

		UpdateSoul();
		StartCoroutine (DayCycle ());
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

		moonParent.transform.Rotate(new Vector3(0, 0, -degreeRotation) * Time.deltaTime);
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
            yield return new WaitForSeconds (nightlength);
            //night stuff here
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

