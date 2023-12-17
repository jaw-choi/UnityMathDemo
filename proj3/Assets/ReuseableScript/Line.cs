using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]

struct Dvec3
{
    public double x;
    public double y;
    public double z;
}
public class Line : MonoBehaviour
{
    public Color baseColor;
    public GameObject target;
    public Material material;

    [SerializeField] private Button Resetbutton;
    [SerializeField] private Slider slider;

    //[SerializeField] private Button Resetbutton;

    public List<Vector3> points = new List<Vector3>();


    Vector3[,] res;

    [SerializeField] TemplatePointPlacer pointplaceController;
    public Vector3 mousePos;

    private void Start()
    {


    }


    void Update()
    {


        Resetbutton.onClick.AddListener(ResetActions);


        if (pointplaceController != null)
        {
            if (pointplaceController.degree < 21)
            {
                res = new Vector3[pointplaceController.degree + 1, pointplaceController.degree + 1];
                for (int a = 0; a <= pointplaceController.degree; a++)
                {
                    res[0, a] = pointplaceController.pointPool[a].transform.position;
                }
            }
        }
        if (points.Count < 21)
            if (Input.GetMouseButtonDown(1))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                if (pointplaceController != null)
                {
                    if (pointplaceController.degree < 21)
                    {
                        points.Add(pointplaceController.pointPool[pointplaceController.degree].transform.position);

                    }
                }

            }
        if (pointplaceController != null)
        {
            if (pointplaceController.degree < 21)
            {


                if (pointplaceController.degree > -1)
                {
                    for (int i = 0; i <= pointplaceController.degree; i++)
                    {
                        points[i] = pointplaceController.pointPool[i].transform.position;
                    }
                }

            }
        }


    }

    void OnRenderObject()
    {
        if (points.Count > 0)
        {
            RenderLines(points, baseColor);
            RenderLines1(points, baseColor);
        }
    }
    virtual public void RenderLines(List<Vector3> points, Color color)
    {
        GL.Begin(GL.LINES);
        material.SetPass(0);
        for (int i = 0; i < points.Count - 1; i++)
        {
            GL.Color(Color.white);
            GL.Vertex(points[i]);

            //GL.Color(material.color);
            GL.Vertex(points[i + 1]);
        }
        GL.End();

    }
    virtual public void RenderLines1(List<Vector3> points, Color color)
    {
        List<Dvec3> tmp = new List<Dvec3>();
        Dvec3 tmp_;
        for(int i=0;i<points.Count;i++)
        {
            tmp_.x = (double)points[i].x;
            tmp_.y = (double)points[i].y;
            tmp_.z = (double)0;

            tmp.Add(tmp_);
        }
        List<Vector3> res = new List<Vector3>();
        GL.Begin(GL.LINES);
        material.SetPass(0);
        int NumberofPoint = points.Count - 1;
        for (double t = 0; t <= NumberofPoint + 0.01; t += 0.1)
        {
            Dvec3 Pt = NewtonPolynomial(tmp, t);
            Vector3 Pt_;
            Pt_.x = (float)Pt.x;
            Pt_.y = (float)Pt.y;
            Pt_.z = (float)Pt.z;
            res.Add(Pt_);

        }

        for (int i = 0; i < res.Count - 1; i++)
        {
            GL.Color(Color.red);
            GL.Vertex(res[i]);
            GL.Vertex(res[i + 1]);

        }
        GL.End();

    }

    // make no recursion
    Dvec3 DivideDifference(List<Dvec3> points, int begin, int end)
    {
        int size_ = end - begin;
        double x1;
        double x2;
        double y1;
        double y2;
        double k_j;
        Dvec3[,] res = new Dvec3[end + 1, end + 1];

        for (int a = 0; a <= end; a++)
        {
            res[a, a] = points[a];
        }

        for (int i = 1; i < size_; i++)
        {
            for (int j = 1; j <= end; j++)
            {
                for (int k = 0; k <= end; k++)
                {
                    if (k - j == i)
                    {
                        x1 = res[j + 1, k].x;
                        x2 = res[j, k - 1].x;
                        y1 = res[j + 1, k].y;
                        y2 = res[j, k - 1].y;
                        k_j = (double)(k - j);
                        res[j, k].x = (float)((x1 - x2) / k_j);
                        res[j, k].y = (float)((y1 - y2) / k_j);
                    }
                    else
                        continue;
                }
            }
        }

        for (int i = 1; i <= end; i++)
        {
            x1 = res[0 + 1, i].x;
            x2 = res[0, i - 1].x;
            y1 = res[0 + 1, i].y;
            y2 = res[0, i - 1].y;
            res[0, i].x = (float)((x1 - x2) / (double)i);
            res[0, i].y = (float)((y1 - y2) / (double)i);
        }

        return res[begin, end];

    }

    List<Dvec3> NewtonCoefficient(List<Dvec3> points)
    {
        int size_ = points.Count;
        List<Dvec3> coefficient = new List<Dvec3>();
        for (int i = 0; i < size_; i++)
        {
            coefficient.Add(DivideDifference(points, 0, i));
        }
        return coefficient;
    }

    Dvec3 NewtonPolynomial(List<Dvec3> points, double t)
    {
        List<Dvec3> coefficient = NewtonCoefficient(points);
        Dvec3 result = coefficient[0];
        double product = 1.0;
        double cx;
        double cy;
        int size_ = coefficient.Count;
        for (int i = 1; i < size_; i++)
        {
            product = product * (double)(t - (i - 1));
            cx = (double)result.x + ((double)(coefficient[i].x) * product);
            cy = (double)result.y + ((double)(coefficient[i].y) * product);
            result.x = (float)(cx);
            result.y = (float)(cy);
        }
        return result;
    }



    public void ResetActions()
    {
        points.Clear();



        if (pointplaceController != null)
            for (int i = 0; i < 20; i++)
            {
                pointplaceController.pointPool[i].gameObject.SetActive(false);
                pointplaceController.pointPool[i].transform.position = new Vector3(0, 0, 0);
            }
        if (pointplaceController != null)
            pointplaceController.degree = -1;
    }
}