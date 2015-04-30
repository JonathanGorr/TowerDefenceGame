using UnityEngine;
using System.Collections;

public class DayNightColors : MonoBehaviour {
	
	public Color[] Colors;
	private Color currentColor;
	private int colorIndex = 0;
	public float transitionSpeed = 0.01f;
	private Color nextColor;
	//private Light sunMoon;
	
	void Awake()
	{
		//sunMoon = GameObject.Find ("Sun").GetComponent<Light>();

		Camera.main.backgroundColor = currentColor;
		
		if (Colors.Length > 0)
		{
			currentColor = Colors[0];
		}
	}
	
	void FixedUpdate()
	{
		for (int i = 0; i < Colors.Length; i++)
		{
			// Get the currentColor in the Array
			if (currentColor == Colors[i])
			{
				colorIndex = i + 1 == Colors.Length ? 0 : i + 1;
			}
		}

		nextColor = Colors[colorIndex];
		
		// Lerp Color _>
		currentColor = Color.Lerp(currentColor, nextColor, transitionSpeed);
		//change the Skybox color
		Camera.main.backgroundColor = currentColor;
		//assign the ambient color to the current
		RenderSettings.ambientLight = currentColor;
		//sunMoon.color = currentColor;
	}
}
