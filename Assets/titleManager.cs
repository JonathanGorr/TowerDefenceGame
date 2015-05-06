using UnityEngine;
using System.Collections;

public class titleManager : MonoBehaviour {
	
	
	void Update()
	{

	}


	public void Start()
	{
		Application.LoadLevel("Staging");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
