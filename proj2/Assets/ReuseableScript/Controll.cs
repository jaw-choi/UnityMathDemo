using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controll : MonoBehaviour
{
    public int UseBB_form => whichForm;

    [SerializeField] Text textForm;
    [SerializeField] Text textT_Value;
    [SerializeField] Button NLI_form;
    [SerializeField] Button BB_form;
    [SerializeField] Button MP_form;
    [SerializeField] Slider SliderTvalue;

    private int whichForm;

    // Start is called before the first frame update
    void Start()
    {
        whichForm = 0;

        NLI_form.onClick.AddListener(() => { whichForm = 0; });
        BB_form.onClick.AddListener(() => { whichForm = 1; });
        MP_form.onClick.AddListener(() => { whichForm = 2; });

    }

    // Update is called once per frame
    void Update()
    {
        if (whichForm == 0)
            textForm.text = "NLI Form Use";
        else if (whichForm == 1)
            textForm.text = "BB Form Use";
        else
            textForm.text = "Mid Point Form Use";

        textT_Value.text = "t = " + SliderTvalue.value.ToString();
    }
}
