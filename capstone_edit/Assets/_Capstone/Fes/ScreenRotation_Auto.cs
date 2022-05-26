using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScreenRotation_Auto : MonoBehaviour
{
    bool isPortrait = true;
    bool isPortrait_initiate;

    bool isKor;

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    int curIndex;

    // Start is called before the first frame update
    void Start()
    {
        CurIndexSetting();

        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            isPortrait = true;
            isPortrait_initiate = true;
        }
        else
        {
            isPortrait_initiate = false;
            isPortrait = false;
        }
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void Update()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft && isPortrait == true) // 세로 -> 가로 전환
        {
            GameObject.Find("CarouselPanel_LS").GetComponent<CarouselView>().GoToIndex(curIndex);

            Debug.Log("Changed: 세로 -> 가로");
            isPortrait = false;
        }
        if(Screen.orientation == ScreenOrientation.Portrait && isPortrait == false) // 가로 -> 세로 전환
        {
            GameObject.Find("CarouselPanel_P").GetComponent<CarouselView>().GoToIndex(curIndex);

            Debug.Log("Changed: 가로 -> 세로 ");
            isPortrait = true;
        }

        CurIndexSetting();

    }

    void CurIndexSetting()
    {
        if (Screen.width <= Screen.height)
        {
            curIndex = Int32.Parse(curImgNumText_Portrait.text);
        }
        else
        {
            curIndex = Int32.Parse(curImgNumText_Landscape.text);
        }

    }

    public bool GetIsPortrait_Initiate()
    {
        return isPortrait_initiate;
    }
}
