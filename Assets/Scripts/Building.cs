﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Building : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;
	public GameObject
		dirtPrefab,
		stonePrefab,
		woodPrefab,
		representation;

	private GameObject selected;

	private GameObject rep;
	private Transform parent;
	private RepCollision repCollision;
	private LevelManager manager;
	private Camera camera;

	public LayerMask blockLayer;

	private Button
		dirt,
		stone,
		wood;

	//snaps
	private Vector3 prevPosition;
	public float snapValue = 1;

	private bool building, canBuild;
	
	void Awake()
	{
		camera = GameObject.Find ("MainCamera").GetComponent<Camera>();
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();

		//early out
		if (manager.inMenu)
			return;

		dirt = GameObject.Find ("Dirt").GetComponent<Button> ();
		parent = GameObject.Find ("Cubes").transform;

		rep = Instantiate (representation, Input.mousePosition, Quaternion.identity) as GameObject;
		repCollision = rep.GetComponent<RepCollision> ();
	}

	void Update () {

		//early out
		if (manager.inMenu)
			return;

		ray = camera.ScreenPointToRay(Input.mousePosition);

		building = true;

		if (manager.souls > 0)
			canBuild = true;
		else
			canBuild = false;

		if(canBuild)
		{
			if(building)
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
	}

	public void Dirt()
	{
		selected = dirtPrefab;
	}
	public void Stone()
	{
		selected = stonePrefab;
	}
	public void Wood()
	{
		selected = woodPrefab;
	}

	private void InstantiateObject()
	{
		GameObject obj = Instantiate(selected, Snap(), Quaternion.identity) as GameObject;

		obj.transform.parent = parent;

		if(selected == dirtPrefab)
			manager.SubtractSoul (1);
		else if(selected == woodPrefab)
			manager.SubtractSoul (2);
		else
			manager.SubtractSoul (3);
	}

	private Vector3 Snap()
	{
		var t = new Vector3();
		t.x = Round( hit.point.x );
		t.y = Round( hit.point.y );
		t.z = Round( hit.point.z );

		//rounded values + the offset to right grid points
		return t + new Vector3(0, 0.5f, 0);
	}
	
	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}
}
