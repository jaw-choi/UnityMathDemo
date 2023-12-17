using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    [Range(0, 1)]

    public float Test;

    public Vector3 P1;
    public Vector3 P2;
    public Vector3 P3;
    public Vector3 P4;

    public Vector3 BezierTest(
        Vector3 P_1,
        Vector3 P_2,
        Vector3 P_3,
        Vector3 P_4,
        float Value
        )
    {
        Vector3 A = Vector3.Lerp(P_1, P_2, Value);
        Vector3 B = Vector3.Lerp(P_1, P_2, Value);
        Vector3 C = Vector3.Lerp(P_1, P_2, Value);

        Vector3 D = Vector3.Lerp(P_1, P_2, Value);
        Vector3 E = Vector3.Lerp(P_1, P_2, Value);

        Vector3 F = Vector3.Lerp(P_1, P_2, Value);

        return F;
    }


    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        linePrefab.transform.position = BezierTest(P1, P2, P3, P4, Test);
        if(Input.GetMouseButtonDown(0))
        {
        GameObject go = Instantiate(linePrefab);
        lr = go.GetComponent<LineRenderer>();
        col = go.GetComponent<EdgeCollider2D>();
        points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lr.positionCount=1;
        lr.SetPosition(0,points[0]);
        }
        else if(Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            points.Add(pos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount-1,pos);
            col.points = points.ToArray();
        }else if(Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }
}
