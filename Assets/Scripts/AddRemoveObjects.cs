using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class AddRemoveObjects : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public GameObject clone, representation;
	private GameObject rep;
	private Transform parent;

	private Toggle toggle;

	//snaps
	private Vector3 prevPosition;
	public float snapValue = 0.5f;
	
	void Awake()
	{
		toggle = GameObject.Find("BuildToggle").GetComponent<Toggle>();
		toggle.isOn = false;
		parent = GameObject.Find("Cubes").transform;

		rep = Instantiate(representation, Input.mousePosition, Quaternion.identity) as GameObject;
		//rep.transform.position = new Vector3(0.5f,0.5f,0.5f);
	}

	void Update () {

		//TODO:
		//check if representational cube is in another cube
		//if so, right click to delete,
		//cannot instantiate if left clicked again
		//can build on top if clicked again

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(toggle.isOn == true)
		{
			if(Physics.Raycast(ray,out hit))
			{
				rep.transform.position = Snap ();

				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					Snap();
					InstantiateObject();
				}
			}
		}
	}

	private void InstantiateObject()
	{
		GameObject obj = Instantiate(clone, Snap(), Quaternion.identity) as GameObject;

		obj.transform.parent = parent;
	}

	private Vector3 Snap()
	{
		var t = new Vector3();
		t.x = Round( hit.point.x );
		t.y = 0.5f; //this should be static to avoid a glitch
		t.z = Round( hit.point.z );
		return t + new Vector3(0.5f, 0, 0.5f);
	}
	
	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}
}
