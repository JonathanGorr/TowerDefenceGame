using UnityEngine;
using System.Collections;

public class fadeOut : MonoBehaviour {

	public float delay = 0f;
	public float duration = .5f;
	
	void Start () {
		StartCoroutine(FadeTo(0.0f, duration));
	}
	
	IEnumerator FadeTo(float aValue, float aTime)
	{
		yield return new WaitForSeconds(delay);

		float alpha = transform.GetComponent<Renderer>().material.color.a;
		for (float t = 0.0f; t <= duration; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			transform.GetComponent<Renderer>().material.color = newColor;

		if(newColor.a <= 0.05)
			Destroy(gameObject);
		yield return null;
		}
	}
}
