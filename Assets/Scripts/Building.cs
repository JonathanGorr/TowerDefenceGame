using UnityEngine;
using UnityEditor;
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
		dirt = GameObject.Find("Dirt").GetComponent<Button>();
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		parent = GameObject.Find("Cubes").transform;

		rep = Instantiate(representation, Input.mousePosition, Quaternion.identity) as GameObject;
		repCollision = rep.GetComponent<RepCollision>();
	}

	void Update () {

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		building = true;

		if (manager.currency > 0)
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
