using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	public GameObject prefab;
	private GameObject shadow;
	private Vector3 location;
	private MeshRenderer renderer;
	private GameObject shadows;

	void Awake()
	{
		shadows = GameObject.Find ("Shadows");
		shadow = Instantiate( prefab, location, Quaternion.identity) as GameObject;
		shadow.name = "Shadow";
		renderer = shadow.GetComponent<MeshRenderer> ();
		shadow.transform.rotation = Quaternion.Euler(90,0,0);

		//make the shadow a child of the parent
		shadow.transform.parent = transform.parent.transform;
	}

	//idk why this works...
	void LateUpdate()
	{
		float yDistance = Mathf.Clamp(Mathf.Abs(transform.position.y), 1.5f, 3f);
		shadow.transform.localScale = new Vector3(yDistance, yDistance, 1);
		renderer.material.color = new Color(0, 0, 0, 1 - yDistance/3); //how the fuck dude!!! MAGIC
		location = new Vector3(transform.position.x, 0.01f, transform.position.z);
		shadow.transform.position = location;
	}
}
