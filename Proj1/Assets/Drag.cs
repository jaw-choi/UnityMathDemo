using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;

public class Drag : MonoBehaviour
{
	public GameObject target;

	// Update is called once per frame

	Vector3 screenPoint;
	Vector3 original;

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		original = target.transform.position;
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		if(curPosition.y<6.0 && curPosition.y >-6.0)
        {
            transform.position = new Vector3(transform.position.x, curPosition.y, -2);
        }
	}
}
