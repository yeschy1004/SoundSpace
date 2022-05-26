using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScreenOrientation_Setting : MonoBehaviour
{
    public GameObject panel_portrait;
    public GameObject panel_landscapeLeft;

    // Start is called before the first frame update
    void Start()
    {
        SetActive_Panel();
    }

    // Update is called once per frame
    void Update()
    {
        SetActive_Panel();
    }


    void SetActive_Panel()
    {
        if (Screen.width <= Screen.height) // portrait
        {
            panel_portrait.SetActive(true);
            panel_landscapeLeft.SetActive(false);
        }
        else // landscape left
        {
            panel_portrait.SetActive(false);
            panel_landscapeLeft.SetActive(true);
        }
    }
}
