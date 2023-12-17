using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlMethodButton : MonoBehaviour
{
    public bool UseBB_form => useBB_form;

    [SerializeField] Text text;
    [SerializeField] Button BB_form;
    [SerializeField] Button NLI_form;

    private bool useBB_form;

    // Start is called before the first frame update
    void Start()
    {
        useBB_form = false;

        BB_form.onClick.AddListener(() => { useBB_form = true; });
        NLI_form.onClick.AddListener(() => { useBB_form = false; });
    }

    // Update is called once per frame
    void Update()
    {
        if (useBB_form)
            text.text = "BB Form Use";
        else
            text.text = "NLI Form Use";
    }
}
