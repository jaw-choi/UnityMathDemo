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
    public List<float> coeffAs = new List<float>();
    public List<float> coeffBs = new List<float>();
    

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
    public void swap(ref float x, ref float y)
    {
        float tempswap = x;
        x = y;
        y = tempswap;
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
        List<Vector3> result = new List<Vector3>();

        if (points.Count>1)
            result = SplineFunctionPoint(points);
        GL.Begin(GL.LINES);
        material.SetPass(0);

        for (int i = 0; i < result.Count - 1; i++)
        {
            GL.Color(Color.red);
            GL.Vertex(result[i]);
            GL.Vertex(result[i + 1]);
        }
        GL.End();
    }


    public double MakeSplineFunctionDoublePrimeK(int t, int index_, int k)
    {
        if (index_ == 0)
            return 0f;
        if (index_ == 1)
            return 0f;
        if (index_ == 2)
            return 2;
        if (index_ == 3)
            return 6 * k;

        double result = 0;
        if (k - (index_ - 3) <= 0)
            return 0;

        result = 6 * (k - (index_ - 3));
        return result;
    }
    public double MakeSplineFunction(int t, int index_, int k)
    {
        if (index_ == 0)
            return 1;
        if (index_ == 1)
            return t;
        if (index_ == 2)
            return t*t;
        if (index_ == 3)
            return t*t*t;

        double result = 0;
        if ((t - (index_ - 3)) > 0)
            result = (t - (index_ - 3)) * (t - (index_ - 3)) * (t - (index_ - 3));
        return result;
    }
    public double MakeSplineFunctionDoublePrimeZero(int t)
    {
        if (t == 2)
            return 2;
        else
            return 0;
    }
    public double MakeSplineFunctionPoint(double t, int index_, double[] Xa)
    {
        double result = 0;
        if (index_ == 0)
            return 1f * Xa[index_];
        else if (index_ == 1)
            return t * Xa[index_];
        else if (index_ == 2)
            return t * t * Xa[index_];
        else if (index_ == 3)
            return t * t * t * Xa[index_];
        else
        {
            if((t - (index_ - 3))>0)
                return (t - (index_ - 3)) * (t - (index_ - 3)) * (t - (index_ - 3)) * Xa[index_];
        }
        return result;
    }
    public List<Vector3> SplineFunctionPoint(List<Vector3> pts)
    {
        List<Vector3> result = new List<Vector3>();
        int size_ = pts.Count;
        int k = pts.Count - 1;
        int n = k + 3;
        double[,] x_matrix = new double[n, n + 1];
        double[,] y_matrix = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                x_matrix[i, j] = MakeSplineFunction(i,j,k);
                y_matrix[i, j] = MakeSplineFunction(i,j,k);
                if (i == n - 2)
                {
                    x_matrix[i, j] = MakeSplineFunctionDoublePrimeZero(j);
                    y_matrix[i, j] = MakeSplineFunctionDoublePrimeZero(j);
                }
                if (i==n-1)
                {
                    x_matrix[i, j] = MakeSplineFunctionDoublePrimeK(i,j,k);
                    y_matrix[i, j] = MakeSplineFunctionDoublePrimeK(i,j,k);
                }
            }
        }

        double[] XaVec = new double[n];
        double[] YaVec = new double[n];
        for (int i = 0; i < n; i++)
        {
            if(i>=pts.Count)
            {
                x_matrix[i, n] = 0f;
                y_matrix[i, n] = 0f;
            }
            else
            {
                x_matrix[i, n] = pts[i].x;
                y_matrix[i, n] = pts[i].y;
            }

        }
        PerformOperation(x_matrix, n);
        PerformOperation(y_matrix, n);
        XaVec = GetResult(x_matrix, n);
        YaVec = GetResult(y_matrix, n);
        double resultX = 0;
        double resultY = 0;
        
        for(double t =0;t<=k;t+=0.01f)
        {
            resultX = 0;
            resultY = 0;
            for (int i = 0; i < XaVec.Length; i++)
            {
                resultX += MakeSplineFunctionPoint(t, i, XaVec);
                resultY += MakeSplineFunctionPoint(t, i, YaVec);
            }
            result.Add(new Vector3((float)resultX, (float)resultY, 1f));
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
    static void PerformOperation(double[,] a, int n)
    {
        int i, j, k = 0, c = 0;

        // Performing elementary operations
        for (i = 0; i < n; i++)
        {
            if (a[i, i] == 0)
            {
                c = 1;
                while ((i + c) < n && a[i + c, i] == 0)
                    c++;
                if ((i + c) == n)
                {
                    break;
                }
                for (j = i, k = 0; k <= n; k++)
                {
                    double temp = a[j, k];
                    a[j, k] = a[j + c, k];
                    a[j + c, k] = temp;
                }
            }

            for (j = 0; j < n; j++)
            {

                // Excluding all i == j
                if (i != j)
                {

                    // Converting Matrix to reduced row
                    // echelon form(diagonal matrix)
                    double p = a[j, i] / a[i, i];

                    for (k = 0; k <= n; k++)
                        a[j, k] = a[j, k] - (a[i, k]) * p;
                }
            }
        }
    }

    public double[] GetResult(double[,]a, int n)
    {
        double[] result = new double[n];
        for (int i = 0; i < n; i++)
            result[i] = a[i, n] / a[i, i];
        return result;
    }


}
