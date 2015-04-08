using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPlayer : Pathfinding
{
    public Camera playerCam;
    public Camera minimapCam;
	private float maxY;


	private void Awake()
	{
		maxY = transform.position.y;//-Mathf.Infinity;
	}

	void Update ()
    {
        FindPath();
        if (Path.Count > 0)
        {
            MoveMethod();
        }
	}

    private void FindPath()
	{
        if (Input.GetButtonDown("Fire1"))
        {
            //Call minimap
            Ray ray = playerCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                FindPath(transform.position, hit.point);
            }      
        }
    }

    private void MoveMethod()
    {
        if (Path.Count > 0)
        {
            Vector3 direction = (Path[0] - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * 14F);
            if (transform.position.x < Path[0].x + 0.4F && transform.position.x > Path[0].x - 0.4F && transform.position.z > Path[0].z - 0.4F && transform.position.z < Path[0].z + 0.4F)
            {
                Path.RemoveAt(0);
            }

            RaycastHit[] hit = Physics.RaycastAll(transform.position + (Vector3.up * 20F), Vector3.down, 100);
			//float maxY = transform.position.y;//-Mathf.Infinity;
            
			/*
			foreach (RaycastHit h in hit)
            {
                if (h.transform.tag == "Untagged")
                {
                    if (maxY < h.point.y)
                    {
                        maxY = h.point.y;
                    }
                }
            }
            */

            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }
}
