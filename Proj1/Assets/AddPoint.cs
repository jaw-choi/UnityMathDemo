using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPoint : MonoBehaviour
{
    [SerializeField] private ControlMethodButton methodController;
    [SerializeField] private Button button;
    [SerializeField] private GameObject pointsHolder;
    [SerializeField] private TemplatePoint pointPrefab;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    //[SerializeField] private lr_LineController pointsController;
    [SerializeField] private lr_LineController controller;
    public TemplatePoint[] pointPool;

    Vector2 startPos;
    Vector2 endPos;
    float length;
    public int degree;
    float t;
    float mt;
    public GameObject GetPointObject(int index)
    {
        return pointPool[index].gameObject;
    }

    void Start()
    {
        pointPool = new TemplatePoint[21];
        button = GetComponent<Button>();
        pointPrefab.gameObject.SetActive(false);
        controller.lr.positionCount = 2;
        controller.lr.SetPosition(0, new Vector2(startPos.x, startPos.y));
        controller.lr.SetPosition(1, new Vector2(endPos.x, endPos.y));
        button.onClick.AddListener(Actions);
        for (int i = 0; i < 21; i++)
        {
            pointPool[i] = Instantiate(pointPrefab, pointsHolder.transform);
            pointPool[i].gameObject.SetActive(false);
            //sphere[i] = Instantiate(pointPrefab,this.transform).GetComponent<>;
            //sphere[i].gameObject.SetActive(false);
        }
        degree = 1;
        startPos = startPoint.transform.position;
        endPos = endPoint.transform.position;
        length = endPos.x - startPos.x;
        t = length / 100f;
        mt = 1f / 100f;
        controller.lr.positionCount = 101;
        pointPool[0].transform.position = new Vector3(startPos.x, startPos.y, -2);
        pointPool[1].transform.position = new Vector3(endPos.x, endPos.y, -2);
        pointPool[0].gameObject.SetActive(true);
        pointPool[1].gameObject.SetActive(true);

        //point.transform.SetParent(pointsHolder.transform);

    }
    float functionPt(int degree_,int i, float t_)
    {
        int origin = degree_;
        
        float[,] res = new float[degree_+1, degree_+1];
        for (int a = 0; a <= degree_; a++) 
        {
            res[0, a] = pointPool[a].transform.position.y;
        }

        for(int j=0;j<degree_;j++)
        {
            for (int k = 0; k < degree_; k++)
            {
                res[j + 1, k] = (1-t_) * res[j, k] + t_ * res[j, k + 1];
            }
        }
        return res[origin, 0];
        
    }
    float functionPtBB(int degree_, float t_)
    {
        float res = 0;
        int[,] pascal = new int[degree_ + 1,degree_+1];
        for (int j = 0; j <= degree_; j++)
        {
            pascal[j, 0] = 1;
        }
        for (int j = 1; j <= degree_; j++)
        {
            for (int k = 0; k <= degree_; k++)
            {
                if(k==0)
                    pascal[j, k] = pascal[j - 1, k];
                else
                    pascal[j, k] = pascal[j - 1, k - 1] + pascal[j - 1, k];
            }
        }

        for (int j = 0; j <= degree_; j++) 
        {

            res += pointPool[j].transform.position.y * pascal[degree_, j] * Mathf.Pow(1 - t_, degree_ - j) * Mathf.Pow(t_, j);

        }
        return res;

    }
    
    // Update is called once per frame
    void Update()
    {
        if(!methodController.UseBB_form)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i <= 100; i++)
                {
                    controller.lr.SetPosition(i, new Vector2(t * i + startPos.x, functionPt(degree, 0, mt * i)));
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i <= 100; i++)
                {
                    controller.lr.SetPosition(i, new Vector2(t * i + startPos.x, functionPtBB(degree, mt * i)));
                }
            }
        }

    }

    public void Actions()
    {
        if (degree < 20)
        {
            degree++;
            //point[0] = Vector2.Lerp(start, end,);
            //controller.lr.positionCount++;
            //float length = (endPos.x - startPos.x) / (controller.lr.positionCount - 1);
            //controller.lr.SetPosition(0, startPos);
            for (int i = 0; i <= degree; i++)
            {
                pointPool[i].gameObject.SetActive(true);
                //controller.lr.SetPosition(i, new Vector2(startPos.x + length * i, pointPool[i].transform.position.y));
                pointPool[i].transform.localPosition = new Vector2(startPos.x + (length/degree) * i, pointPool[i].transform.position.y);

            }
            //points.Add(new Vector2(3,3));
            //controller.lr.SetPosition(controller.lr.positionCount -1, new Vector2(3, 3));
        }

    }
}
