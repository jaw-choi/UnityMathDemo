using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controll : MonoBehaviour
{

    [SerializeField] Text textT_Value;
    [SerializeField] TemplatePointPlacer pointsNum;



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (pointsNum != null)
            textT_Value.text = (pointsNum.degree+1).ToString();
    }
}
