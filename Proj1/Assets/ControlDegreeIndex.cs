using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ControlDegreeIndex : MonoBehaviour
{
    public int Degree => degree;

    [SerializeField] Text text;
    [SerializeField] Button Increase_Button;
    [SerializeField] Button Decrease_Button;

    private int degree;
    // Start is called before the first frame update
    void Start()
    {
        degree = 1;

        Increase_Button.onClick.AddListener(() => { if (degree < 20) degree++; });
        Decrease_Button.onClick.AddListener(() => { if (degree > 1) degree--; });
    }

    // Update is called once per frame
    void Update()
    {
            text.text = "Degree " + degree.ToString();

    }
}
