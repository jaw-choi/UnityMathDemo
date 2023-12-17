using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]

public class Line : MonoBehaviour
{
    public Color baseColor;
    public GameObject target;
    public Material material;
    [SerializeField] private Button NLIbutton;
    [SerializeField] private Button BBbutton;
    [SerializeField] private Button MPbutton;
    [SerializeField] private Button Resetbutton;
    [SerializeField] private Slider slider;

    //[SerializeField] private Button Resetbutton;
    private bool NLI = true;
    private bool BB = false;
    private bool MP = false;
    private float tValue;
    public List<Vector3> points = new List<Vector3>();
    public List<Vector3>[] pointsHalf = new List<Vector3>[18];
    List<Vector3> resultQ = new List<Vector3>();
    List<Vector3> resultR = new List<Vector3>();
    Vector3[,] res;
    public List<Vector3> pointsLine = new List<Vector3>();
    public List<Vector3> pointsLineMP = new List<Vector3>();
    [SerializeField] TemplatePointPlacer pointplaceController;
    public Vector3 mousePos;

    private void Start()
    {
        tValue = slider.value;
        for (int i = 0; i < 18; i++)
            pointsHalf[i] = new List<Vector3>();

    }


    void Update()
    {
        tValue = slider.value;
        //OnRenderObject();
        NLIbutton.onClick.AddListener(NLIActions);
        BBbutton.onClick.AddListener(BBActions);
        MPbutton.onClick.AddListener(MPActions);
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
        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (pointplaceController != null)
            {
                if (pointplaceController.degree < 21)
                {

                    points.Add(pointplaceController.pointPool[pointplaceController.degree].transform.position);

                    if (pointplaceController.degree > 0)
                        pointsHalf[0].Add((pointplaceController.pointPool[pointplaceController.degree - 1].transform.position * (1 - tValue)
                                    + pointplaceController.pointPool[pointplaceController.degree].transform.position) * tValue);
                    if (NLI)
                    {
                        if (pointplaceController.degree > 1)
                        {
                            pointsLine.Clear();
                            for (int i = 0; i < 100; i++)
                            {
                                pointsLine.Add(functionNLI(pointplaceController.degree, 0.01f * i));
                            }
                        }
                    }
                    else if (BB)
                    {
                        if (pointplaceController.degree > 1)
                        {
                            pointsLine.Clear();
                            for (int i = 0; i < 100; i++)
                            {
                                pointsLine.Add(functionBB(pointplaceController.degree, 0.01f * i));
                            }
                        }
                    }
                    else if (MP)
                    {
                        if (pointplaceController.degree > 1)
                        {
                            pointsLineMP.Clear();
                            if (res != null)
                                pointsLineMP = functionMP(pointplaceController.degree, res, 10,0);
                        }
                    }


                    if (pointplaceController.degree > 2)//3
                    {

                        int size = pointplaceController.degree - 2;
                        for (int i = 0; i < size; i++)
                        {
                            pointsHalf[i + 1].Clear();
                            for (int j = 0; j < pointsHalf[i].Count - 1; j++)
                            {
                                pointsHalf[i + 1].Add((pointsHalf[i][j]) * (1 - tValue) + (pointsHalf[i][j + 1]) * tValue);
                            }
                        }
                    }
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
                if (pointplaceController.degree > 1)
                {
                    for (int j = 1; j < pointplaceController.degree - 1; j++)
                    {
                        for (int i = 0; i < pointplaceController.degree - j; i++)
                        {
                            pointsHalf[j][i] = ((pointsHalf[j - 1][i]) * (1 - tValue) + (pointsHalf[j - 1][i + 1]) * tValue);

                        }
                    }
                    for (int i = 0; i < pointplaceController.degree; i++)
                    {
                        pointsHalf[0][i] = ((points[i]) * (1 - tValue) + (points[i + 1]) * tValue);
                    }
                    if (NLI)
                    {
                        //pointsLine.Clear();
                        if (pointsLine.Count > 0)
                            for (int i = 0; i < 100; i++)
                        {
                            pointsLine[i] = functionNLI(pointplaceController.degree, 0.01f * i);
                        }
                    }
                    if (BB)
                    {
                        //pointsLine.Clear();
                        if(pointsLine.Count>0)
                        for (int i = 0; i < 100; i++)
                        {
                            pointsLine[i] = functionBB(pointplaceController.degree, 0.01f * i);
                        }
                    }
                    if (MP)
                        if(res!=null)
                        {
                            pointsLineMP.Clear();
                            pointsLineMP = functionMP(pointplaceController.degree, res, 10,0);
                        }

                }
            }
        }
        

    }

    void OnRenderObject()
    {

            RenderLines(points, baseColor);
            if (NLI)
                for (int i = 0; i < 18; i++)
                {
                    RenderLines1(pointsHalf[i], baseColor);
                }

            if (MP)
                RenderLines2(pointsLineMP, baseColor);
            else
                RenderLines2(pointsLine, baseColor);
        


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

        GL.Begin(GL.LINES);
        material.SetPass(0);
        for (int i = 0; i < points.Count - 1; i++)
        {
            GL.Color(Color.red);
            GL.Vertex(points[i]);

            //GL.Color(material.color);
            GL.Vertex(points[i + 1]);
        }
        GL.End();

    }
    virtual public void RenderLines2(List<Vector3> points, Color color)
    {
        GL.Begin(GL.LINES);
        material.SetPass(0);
        for (int i = 0; i < points.Count - 1; i++)
        {
            GL.Color(Color.black);
            GL.Vertex(points[i]);

            //GL.Color(material.color);
            GL.Vertex(points[i + 1]);
        }
        GL.End();

    }
    Vector3 functionNLI(int degree_, float t_)
    {
        int origin = degree_;

        Vector3[,] res = new Vector3[degree_ + 1, degree_ + 1];
        for (int a = 0; a <= degree_; a++)
        {
            res[0, a] = pointplaceController.pointPool[a].transform.position;
        }

        for (int j = 0; j < degree_; j++)
        {
            for (int k = 0; k < degree_; k++)
            {
                res[j + 1, k] = (1 - t_) * res[j, k] + t_ * res[j, k + 1];
            }
        }
        return res[origin, 0];
    }

    Vector3 functionBB(int degree_, float t_)
    {
        Vector3 res = new Vector3();
        int[,] pascal = new int[degree_ + 1, degree_ + 1];
        for (int j = 0; j <= degree_; j++)
        {
            pascal[j, 0] = 1;
        }
        for (int j = 1; j <= degree_; j++)
        {
            for (int k = 0; k <= degree_; k++)
            {
                if (k == 0)
                    pascal[j, k] = pascal[j - 1, k];
                else
                    pascal[j, k] = pascal[j - 1, k - 1] + pascal[j - 1, k];
            }
        }

        for (int j = 0; j <= degree_; j++)
        {

            res += pointplaceController.pointPool[j].transform.position * pascal[degree_, j] * Mathf.Pow(1 - t_, degree_ - j) * Mathf.Pow(t_, j);

        }
        return res;

    }

    List<Vector3> functionMP(int degree_, Vector3[,] P, int k,int flag)
    {
        if (k == 0)
        {
            List<Vector3> tmp = new List<Vector3>();
            for (int j = 0; j <= degree_; j++)
            {
                if (flag == 1)
                    resultQ.Add(P[0, j]);
                else if (flag == 2)
                    resultR.Add(P[0, j]);
            }

            return tmp;
        }

        Vector3[,] Q = new Vector3[degree_ + 1, degree_ + 1];
        Vector3[,] R = new Vector3[degree_ + 1, degree_ + 1];

        for (int i = 1; i <= degree_; i++)
        {
            for (int j = 0; j <= degree_ - i; j++)
            {
                P[i, j] = (P[i - 1, j] + P[i - 1, j + 1]) / 2f;
            }
        }
        for (int j = 0; j <= degree_; j++)
        {
            Q[0, j] = P[j, 0];
            R[0, j] = P[degree_ - j, j];
        }

        functionMP(degree_, Q, k - 1,1);
        functionMP(degree_, R, k - 1,2);

        return AddList(resultQ, resultR);
    }
    List<Vector3> AddList(List<Vector3> A, List<Vector3> B)
    {

        for (int i = 1; i < B.Count; i++)
        {
            A.Add(B[i]);
        }
        B.Clear();
        return A;
    }


    public void NLIActions()
    {
        NLI = true;
        BB = false;
        MP = false;
    }
    public void BBActions()
    {
        NLI = false;
        BB = true;
        MP = false;
    }
    public void MPActions()
    {
        NLI = false;
        BB = false;
        MP = true;
    }
    public void ResetActions()
    {
        points.Clear();
        for (int i = 0; i < 18; i++)
            pointsHalf[i].Clear();
        pointsLine.Clear();
        pointsLineMP.Clear();
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