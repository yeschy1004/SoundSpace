using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class DotChanger : MonoBehaviour
{
    public Image[] dotsImg;
    public Sprite selectedDotImg;
    public Sprite nonSelectedDotImg;

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;


    int curIndex;
    int preIndex;

    int portrait_NonSelectedDotSize = 22;
    int portrait_SelectedDotSize = 60;

    int landscape_NonSelectedDotSize = 40;
    int landscape_SelectedDotSize = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        preIndex = 0;
        CurIndexSetting();

        dotsImg[curIndex].sprite = selectedDotImg;

        RectTransform rect = (RectTransform)dotsImg[curIndex].transform;

        if (Screen.width <= Screen.height) // portriat
        {
            rect.sizeDelta = new Vector2(portrait_SelectedDotSize, portrait_SelectedDotSize);
        }
        else // landscape
        {
            rect.sizeDelta = new Vector2(landscape_SelectedDotSize, landscape_SelectedDotSize);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CurIndexSetting();

        if (preIndex != curIndex)
        {
            DotImgSizeChanger();
            DotImgChanger();

        }
    }


    #region private function
    void DotImgChanger()
    {
        dotsImg[curIndex].sprite = selectedDotImg;
        dotsImg[preIndex].sprite = nonSelectedDotImg;

        preIndex = curIndex;
    }

    void DotImgSizeChanger()
    {
        RectTransform curRect = (RectTransform)dotsImg[curIndex].transform;
        RectTransform preRect = (RectTransform)dotsImg[preIndex].transform;

        if (Screen.width <= Screen.height) // portriat
        {
            preRect.sizeDelta = new Vector2(portrait_NonSelectedDotSize, portrait_NonSelectedDotSize);
            curRect.sizeDelta = new Vector2(portrait_SelectedDotSize, portrait_SelectedDotSize);
        }
        else // landscape
        {
            preRect.sizeDelta = new Vector2(landscape_NonSelectedDotSize, landscape_NonSelectedDotSize);
            curRect.sizeDelta = new Vector2(landscape_SelectedDotSize, landscape_SelectedDotSize);
        }
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

    #endregion
}
