﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class AddRemoveObjects : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;
	public GameObject clone, representation;
	private GameObject rep;
	private Transform parent;
	private RepCollision repCollision;

	public LayerMask blockLayer;

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
		repCollision = rep.GetComponent<RepCollision>();
	}

	void Update () {

		//TODO:
		//check if representational cube is in another cube
		//if so, right click to delete,
		//cannot instantiate if left clicked again
		//can build on top if clicked again
		//if the player is colliding, cannot place
		//check if mouse is over toggle button, if so, destroy

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(toggle.isOn == true)
		{
			if(Physics.Raycast(ray,out hit, Mathf.Infinity, blockLayer))
			{
				//move the repCube according to the grid
				rep.transform.position = Snap ();

				if(!repCollision.isColWithPlayer)
				{
					if(Input.GetKeyDown(KeyCode.Mouse0))
					{
						InstantiateObject();
					}
				}

				else if(repCollision.isInAnotherCube)
				{
					if(Input.GetKeyDown(KeyCode.Mouse0))
					{
						InstantiateObject();
					}

					if(Input.GetKeyDown(KeyCode.Mouse2))
					{
						//Destroy cube
					}
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
		t.y = Round( hit.point.y );
		t.z = Round( hit.point.z );

		//rounded values + the offset to right grid points
		return t + new Vector3(0.5f, 0.5f, 0.5f);
	}
	
	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}
}
