using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class TemplatePointPlacer : MonoBehaviour
{
    public GameObject pointPrefab;
    public TemplatePoint[] pointPool;
    //[SerializeField] private Button Resetbutton;
    public int degree = -1;

    //Creates Control Points in the Pool

    void Start()
    {
        pointPool = new TemplatePoint[21];
        for (int i = 0; i < 21; i++)
        {
            pointPool[i] = Instantiate(pointPrefab, this.transform).GetComponent<TemplatePoint>();
            pointPool[i].gameObject.SetActive(false);
        }
    }

    //Enables a point on right click
    private void Update()
    {
        //Resetbutton.onClick.AddListener(ResetActions);
        if (Input.GetMouseButtonDown(1))
        {
            if (degree < 20)
            {
                degree++;
                ActivePoint();
            }
        }
    }

    public void ActivePoint()
    {
        for (int i = 0; i < 21; i++)
        {
            if (i <= degree)
            {
                pointPool[i].gameObject.SetActive(true);
            }
            else
            {
                pointPool[i].gameObject.SetActive(false);
            }
        }

        if (degree > -1)
        {
            pointPool[degree].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }
    }
    public void ResetActions()
    {
        pointPool = new TemplatePoint[21];
        for (int i = 0; i < 21; i++)
        {
            pointPool[i] = Instantiate(pointPrefab, this.transform).GetComponent<TemplatePoint>();
            pointPool[i].gameObject.SetActive(false);
        }

    }
}