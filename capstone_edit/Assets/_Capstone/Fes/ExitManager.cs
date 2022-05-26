using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject exitBox_Portrait;
    public GameObject exitBox_Landscape;

    int ClickCount = 0;

    bool isExitMode = false;

    void Start()
    {
        exitPanel.SetActive(false);
        exitBox_Portrait.SetActive(false);
        exitBox_Landscape.SetActive(false);

        isExitMode = false;
    }

    void Update()
    {
        if (isExitMode)
        {
            if (Screen.width <= Screen.height)
            {
                exitBox_Portrait.SetActive(true);
                exitBox_Landscape.SetActive(false);
            }
            else
            {
                exitBox_Portrait.SetActive(false);
                exitBox_Landscape.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isExitMode = true;

            exitPanel.SetActive(true);

            if (Screen.width <= Screen.height)
            {
                exitBox_Portrait.SetActive(true);
                exitBox_Landscape.SetActive(false);
            }
            else
            {
                exitBox_Portrait.SetActive(false);
                exitBox_Landscape.SetActive(true);
            }

        }

        //더블클릭 action
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");


            Application.Quit();
        }
        */
    }


    public void OnClick_CancelBtn()
    {
        exitBox_Portrait.SetActive(false);
        exitBox_Landscape.SetActive(false);
        exitPanel.SetActive(false);

        isExitMode = false;
    }

    public void Onclick_ExitBtn()
    {
        Application.Quit();
    }

    void DoubleClick()
    {
        ClickCount = 0;
    }
}
