using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPoint : MonoBehaviour
{
    [SerializeField] private Button Resetbutton;
    [SerializeField] private List<Vector2> points;
    [SerializeField] private lr_LineController controller;
    // Start is called before the first frame update
    [SerializeField] private AddPoint SphereController;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    Vector2 startPos;
    Vector2 endPos;
    float length;
    public AddPoint control_degree;
    void Start()
    {
        Resetbutton.onClick.AddListener(Actions1);
        startPos = startPoint.transform.position;
        endPos = endPoint.transform.position;
        length = endPos.x - startPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        //control_degree.degree
    }
    public void Actions1()
    {
        if (control_degree.degree > 1)
        {
            SphereController.GetPointObject(control_degree.degree).SetActive(false);
            control_degree.degree--;
            
            //controller.lr.SetPosition(0, new Vector2(startPos.x, startPos.y));
            for (int i = 0; i <= control_degree.degree; i++)
            {
                //controller.lr.SetPosition(i, new Vector2(startPos.x + length * i, SphereController.GetPointObject(i).transform.position.y));
                SphereController.GetPointObject(i).transform.localPosition = new Vector2(startPos.x + (length/ control_degree.degree) * i, SphereController.GetPointObject(i).transform.position.y);
            }

        }


    }

}
