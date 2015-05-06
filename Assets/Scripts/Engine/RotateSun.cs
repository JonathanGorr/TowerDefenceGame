using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateSun : MonoBehaviour {

	//player
	public Transform player;

	//gameobjects
	private GameObject sun, moon;
	private Text dayText;

	//bools
	private bool displayed, vanquished, healed;

	//components
	private LevelManager manager;
	//the players lantern
	private Light lantern;

	//audio
	public AudioClip newDay, nightTime;

	//values
	public float timeScale = 1f;
	public float planetSize = 1f;
	public float lightDirection;
	public Vector3 offset = new Vector3 (0, 0, 35);
	public float radius = 6;
	public float timeRT = 0;
	public float prevDayValue = 0;
	public float transitionDelay = 3f;
	public float healDelay = 3f;
	
	//values
	private int currentDay, lastDay, mostDays, nextUnlock;
	private int unlockday3 = 3;
	private int unlockday6 = 6;
	private int unlockday9 = 9;
	private Text most, next;

	//sky colors
	public Color daytimeSkyColor = new Color(0.31f, 0.88f, 1f);
	public Color middaySkyColor = new Color(0.58f, 0.88f, 1f);
	public Color nighttimeSkyColor = new Color(0.04f, 0.19f, 0.27f);

	//ambient lights
	public Color ambientDayColor = new Color (1f, 0f, 0f);
	public Color ambientMidDayColor = new Color (0f, 1f, 0f);
	public Color ambientNightColor = new Color (0f, 0f, 1f);

	//contant value for times
	public const float daytimeRLSeconds   = 10.0f * 60;
	public const float duskRLSeconds      =  1.5f * 60;
	public const float nighttimeRLSeconds =  7.0f * 60;
	public const float sunsetRLSeconds    =  1.5f * 60;
	public const float gameDayRLSeconds = daytimeRLSeconds + duskRLSeconds + nighttimeRLSeconds + sunsetRLSeconds;

	//states
	[HideInInspector] public enum States{ Morning, Evening, Dusk, Night }
	[HideInInspector] public States currentState;

	//this is used to get/set a state from outside this script
	public States CurrentState
	{
		get { return currentState; }
		set { currentState = value; }
	}

	//starts of times of day
	public const float startOfDaytime = 0;
	public const float startOfDusk = daytimeRLSeconds / gameDayRLSeconds;
	public const float startOfNighttime = startOfDusk + duskRLSeconds / gameDayRLSeconds;
	public const float startOfSunset = startOfNighttime + nighttimeRLSeconds / gameDayRLSeconds;

	void Awake()
	{
		lantern = GameObject.Find ("Player").GetComponentInChildren<Light> ();
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		dayText = GameObject.Find ("DayCanvas").GetComponentInChildren<Text>();
	}

	void Start()
	{
		lantern.intensity = 0.25f;
		// Creating everything needed to demonstrate this from a single cube
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		sun = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sun.name = "sun";
		sun.GetComponent<Renderer> ().material.color = Color.yellow;
		sun.AddComponent<Light> ().type = LightType.Directional;
		sun.GetComponent<Light> ().shadows = LightShadows.Hard;
		sun.GetComponent<Light> ().color = new Color (1, 1, 0.5f);
		sun.GetComponent<Renderer> ().castShadows = false;
		moon = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		moon.name = "moon";
		moon.GetComponent<Renderer> ().material.color = new Color (0.75f, 0.75f, 0.75f);
		moon.AddComponent<Light> ().type = LightType.Directional;
		moon.GetComponent<Light> ().shadows = LightShadows.Hard;
		moon.GetComponent<Light> ().color = new Color (0.5f, 0.5f, 0.5f);
		moon.GetComponent<Light> ().intensity = 1f;
		moon.GetComponent<Renderer> ().castShadows = false;
	}

	public float TimeOfDay // game time 0 .. 1
	{
		get { return timeRT/gameDayRLSeconds; }
		set { timeRT = value * gameDayRLSeconds; }
	}

	IEnumerator DisplayDay(float delay)
	{
		//increment the day
		currentDay ++;
		SoundManager.instance.PlaySingle(newDay);
		dayText.text = "Day " + currentDay;
		dayText.GetComponent<Animator>().SetTrigger("DisplayDay");
		yield return new WaitForSeconds (delay);
		dayText.GetComponent<Animator>().SetTrigger("HideDay");
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

	public void VanquishEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach(GameObject enemy in enemies)
		{
			enemy.GetComponent<EnemyHealth>().StartFade();
		}

		vanquished = true;
	}

	public void UpdateMostDays(int newMost){
		if (most) most.text = "Most Days Survived: " + newMost.ToString (" 00");
		else print ("There is no most");
		
		if(newMost > nextUnlock) { UpdateNextUnlock(newMost); }
	}
	
	IEnumerator HealPlayer()
	{
		PlayerHealth health = player.GetComponent<PlayerHealth>();

		while(currentState == States.Evening && health.health < health.maxHealth)
		{
			healed = true;
			player.GetComponent<PlayerHealth> ().Heal (1);
			//print ("healed");
			yield return new WaitForSeconds(healDelay);
			healed = false;
		}
	}
	
	void Update () {

		//state machine
		if (TimeOfDay > 0.01f && TimeOfDay < 0.02f)
			currentState = States.Morning;
		else if (TimeOfDay > 0.25f && TimeOfDay < 0.26f)
			currentState = States.Evening;
		else if (TimeOfDay > 0.5f && TimeOfDay < 0.51f)
			currentState = States.Dusk;
		else if (TimeOfDay > 0.75f && TimeOfDay > 0.76f)
			currentState = States.Night;

		switch (currentState) {

		case States.Morning:
			lantern.intensity = 0.25f;
			//case/switch
			if(!displayed)
				StartCoroutine("DisplayDay", transitionDelay); displayed = true;
			if(!vanquished)
				VanquishEnemies();
			break;
				
		case States.Evening:
			lantern.intensity = 0.25f;
			if(!healed)
				StartCoroutine("HealPlayer");
			break;
				
		case States.Dusk:
			lantern.intensity = .5f;
			break;
				
		case States.Night:
			//set these bools back to false
			displayed = false;
			vanquished = false;
			lantern.intensity = 1;
			break;
		}

		TimeOfDay += timeScale/1000;
		timeRT = (timeRT + Time.deltaTime) % gameDayRLSeconds;
		Camera.main.backgroundColor = CalculateSkyColor();
		RenderSettings.ambientLight = CalculateAmbientColor();
		float sunangle = TimeOfDay * 360;
		float moonangle = TimeOfDay * 360 + 180;
		Vector3 midpoint = transform.position;// = player.position; midpoint.y = offset.y; midpoint.z = offset.z;
		Vector3 size = new Vector3 (planetSize, planetSize, planetSize);
		sun.transform.position = midpoint + Quaternion.Euler(0,0,sunangle)*(radius*Vector3.right);
		sun.transform.localScale = size;
		sun.transform.LookAt(midpoint);
		moon.transform.position = midpoint + Quaternion.Euler(0,0,moonangle)*(radius*Vector3.right);
		moon.transform.localScale = size;
		moon.transform.LookAt(midpoint);
	}
	
	Color CalculateSkyColor()
	{
		float time = TimeOfDay;
		if (time <= 0.25f)
			return Color.Lerp(daytimeSkyColor, middaySkyColor, time/0.25f);
		if (time <= 0.5f)
			return Color.Lerp(middaySkyColor, daytimeSkyColor, (time-0.25f)/0.25f);
		if (time <= startOfNighttime)
			return Color.Lerp(daytimeSkyColor, nighttimeSkyColor, (time-startOfDusk)/(startOfNighttime-startOfDusk));
		if (time <= startOfSunset)
			return nighttimeSkyColor;

		//else
		return Color.Lerp(nighttimeSkyColor, daytimeSkyColor, (time-startOfSunset)/(1.0f-startOfSunset));
	}

	Color CalculateAmbientColor()
	{
		float time = TimeOfDay;
		if (time <= 0.25f)
			return Color.Lerp(ambientDayColor, ambientMidDayColor, time/0.25f);
		if (time <= 0.5f)
			return Color.Lerp(ambientMidDayColor, ambientDayColor, (time-0.25f)/0.25f);
		if (time <= startOfNighttime)
			return Color.Lerp(ambientDayColor, ambientNightColor, (time-startOfDusk)/(startOfNighttime-startOfDusk));
		if (time <= startOfSunset) 
			return nighttimeSkyColor;

		//else
		return Color.Lerp(ambientNightColor, ambientDayColor, (time-startOfSunset)/(1.0f-startOfSunset));
	}
	
	void OnGUI()
	{
		Rect rect = new Rect(10, 10, 120, 20);
		//GUI.Label(rect, "time: " + TimeOfDay); rect.y+=20;
		//GUI.Label(rect, "timeRT: " + timeRT);
		rect = new Rect(120, 10, 200, 10);
		TimeOfDay = GUI.HorizontalSlider(rect, TimeOfDay, 0, 1);
	}
}