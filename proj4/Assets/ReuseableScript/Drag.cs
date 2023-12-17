using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;
/**
License: do whatever you want.
Author: @t_machine_org
*/
public class Drag : MonoBehaviour
{
	public GameObject target;
	[SerializeField] private Line pointsController;
	// Update is called once per frame
	
	Vector3 screenPoint;
	Vector3 original;
	//bool isPressed;
	//int index;
	private void Start()
	{

	}
    private void Update()
    {
        
    }
    void OnMouseDown()
	{
		//isPressed = true;
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		original = target.transform.position;
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		
		transform.position = curPosition; 
		if(pointsController!= null)
        {
			for (int i = 0; i < pointsController.points.Count; i++)
			{
				if (pointsController.points[i] == transform.position)
                {
					//index = i;
					//transform.position = curPosition;
					//pointsController.points[i] = transform.position;
					//pointsController.points.Add(curPosition);
				}

			}
		}
		//pointsController.points[index] = curPosition;

	}
}
