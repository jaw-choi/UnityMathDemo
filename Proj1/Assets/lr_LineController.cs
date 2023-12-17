using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{
    public GameObject linePrefab;
    public LineRenderer lr;
    //private Transform[] points;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    public List<Vector2> points = new List<Vector2>();
    Vector2 startPos => startPoint.transform.position;
    Vector2 endPos => endPoint.transform.position;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine(List<Vector2> points){
        lr.positionCount = 2;
        this.points = points;

    }

  
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(linePrefab);
        lr = go.GetComponent<LineRenderer>();
        //points.Add(startPos);
        //points.Add(endPos);
        //lr.SetPosition(0, points[0]);
        //lr.SetPosition(1, points[1]);



    }

    // Update is called once per frame
    void Update()
    {
        //GameObject go = Instantiate(linePrefab);
        //lr = go.GetComponent<LineRenderer>();
        //points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //lr.positionCount = 1;
        //lr.SetPosition(0, points[0]);
        //if(Input.GetMouseButtonDown(0))
        //{
        //    //GameObject point;
        //    Vector3 pos = Input.mousePosition;
        //    var worldPosition = Camera.main.ScreenToWorldPoint(pos);
        //    points.Add(worldPosition);
        //}
        //for (int i=0; i< points.Count;i++){
        //    //lr.SetPosition(i,points[i]);
        //    lr.SetPosition(lr.positionCount - 1, points[i]);
        //}
    }
}
