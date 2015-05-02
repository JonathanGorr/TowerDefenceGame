using UnityEngine;
using System.Collections;

public class DayNightSystem : MonoBehaviour {

	//constants
	public float dayCycleInMinutes = .1f;
	public const float SECOND = 1;
	public const float MINUTE = 60 * SECOND;
	public const float HOUR = 60 * MINUTE;
	public const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;
	private float degreeRotation;

	//values
	public float daylength = 3f;
	public float nightlength = 3f;
	public float skyTransitionSpeed = 0.06f;

	//colors
	public Color nightColor;
	public Color dayColor;
	private Color currentColor;
	private Color nextColor;

	private GameObject moonParent;

	//components
	private LevelManager manager;

	//sounds
	public AudioClip newDay;
	public AudioSource nightTrack1;

	void Awake()
	{
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		//set constants for spinning moon/day cycle
		//spinRate = daylength + nightlength;
		//dayCycleInMinutes = .1f;
		degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);
		StartCoroutine (DayCycle ());

		currentColor = dayColor;
		Camera.main.backgroundColor = currentColor;
		RenderSettings.ambientLight = currentColor;
	}

	void Update()
	{
		currentColor = Color.Lerp(currentColor, nextColor, skyTransitionSpeed);
		Camera.main.backgroundColor = currentColor;
		RenderSettings.ambientLight = currentColor;

		if(moonParent)
			moonParent.transform.Rotate(new Vector3(0, 0, -degreeRotation) * Time.deltaTime);
	}

	IEnumerator DayCycle ()
	{
		//get all the enemies alive
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		//kill them all
		foreach(GameObject enemy in enemies)
		{
			enemy.GetComponent<EnemyHealth>().OnKill();
		}
		
		while (true)
		{
			if(manager)
				//manager.UpdateDay(1);
			
			yield return new WaitForSeconds (daylength);
			//day stuff here
			nextColor = dayColor;
			manager.canvas.alpha = 1f;
			SoundManager.instance.PlaySingle (newDay);
			nightTrack1.Stop();
			yield return new WaitForSeconds (3);
			manager.canvas.alpha = 0f;
			yield return new WaitForSeconds (nightlength);
			//night stuff here
			nightTrack1.Play();
			nextColor = nightColor;
		}
	}
}
