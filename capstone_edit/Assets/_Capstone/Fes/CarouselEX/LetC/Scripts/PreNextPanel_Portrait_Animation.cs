using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


//패널 위치 조정(선택: -250, 선택x : -275 정도?)
//다른 패널 투명도 설정

public class PreNextPanel_Portrait_Animation : MonoBehaviour
{
    public GameObject carouselObj;
    public GameObject[] images;
    public GameObject[] images_thumbnail;
    public GameObject[] images_guide;

    public GameObject[] images_guide1;
    public GameObject[] images_guide2;
    public Text[] texts_guide1;
    public Text[] texts_guide2;

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    public int maxImgSize;

    int defaultIndex = 0;

    int curIndex;
    int preIndex;

    int positionY_default = -205;
    int positionY_changedRange = 130;

    float alpha_changedRange = 1f;
    float alpha_selected = 1f;
    float alpha_nonSelected = 0.2f;

    bool isPortrait_initiate;

    // Start is called before the first frame update
    void Start()
    {
        isPortrait_initiate = GameObject.Find("GameObject123").GetComponent<ScreenRotation_Auto>().GetIsPortrait_Initiate();

        if (Screen.width <= Screen.height) // 세로 모드
        {
            if (!isPortrait_initiate) // 가로 -> 세로 전환
            {
                curIndex = Int32.Parse(curImgNumText_Landscape.text);
                defaultIndex = curIndex;
            }
            else
            {
                curIndex = Int32.Parse(curImgNumText_Portrait.text);
                defaultIndex = 0;
            }
        }
        else //가로 모드
        {
            curIndex = Int32.Parse(curImgNumText_Landscape.text);
            defaultIndex = 0;
        }

        preIndex = defaultIndex;   //시작 콘텐츠

        Debug.Log("defaultIndex: " + defaultIndex);


        Vector3 carouselObjPst = carouselObj.transform.localPosition;
        carouselObjPst.x = 0;
        carouselObjPst.y = positionY_default;
        carouselObj.transform.localPosition = carouselObjPst;

        Debug.Log("세로 모드 앞뒤패널 start");

        for (int i=0; i < maxImgSize; i++){

            // 위치 이동
            if (i != defaultIndex) 
            {
                Vector3 pos = images[i].transform.localPosition;
                pos.x = 0;
                pos.y = -positionY_changedRange;
                images[i].transform.localPosition = pos;

                alpha_changedRange = alpha_nonSelected;
            }    
            else
            {
                alpha_changedRange = alpha_selected;
            }

            // 투명도 조절 
            ChangeImgColor(images_thumbnail[i], alpha_changedRange);
            ChangeImgColor(images_guide[i], alpha_changedRange);
            ChangeImgColor(images_guide1[i], alpha_changedRange);
            ChangeImgColor(images_guide2[i], alpha_changedRange);

            ChangeTxtColor(texts_guide1[i], alpha_changedRange);
            ChangeTxtColor(texts_guide2[i], alpha_changedRange);

        }

    }

    // Update is called once per frame
    void Update()
    {
        CurIndexSetting();
       
        if (preIndex != curIndex)
        {
            Vector3 pos1 = images[preIndex].transform.localPosition;
            pos1.x = 0;
            pos1.y = -positionY_changedRange;
            images[preIndex].transform.localPosition = pos1;

            Vector3 pos2 = images[curIndex].transform.localPosition;
            pos2.x = 0;
            pos2.y = 0;
            images[curIndex].transform.localPosition = pos2;

            for (int i = 0; i < maxImgSize; i++)
            {
                if (i != curIndex)
                {
                    alpha_changedRange = alpha_nonSelected;
                }

                else
                {
                    alpha_changedRange = alpha_selected;
                }

                //투명도 조절

                ChangeImgColor(images_thumbnail[i], alpha_changedRange);
                ChangeImgColor(images_guide[i], alpha_changedRange);
                ChangeImgColor(images_guide1[i], alpha_changedRange);
                ChangeImgColor(images_guide2[i], alpha_changedRange);

                ChangeTxtColor(texts_guide1[i], alpha_changedRange);
                ChangeTxtColor(texts_guide2[i], alpha_changedRange);


                preIndex = curIndex;
            }


        } 
    }

    #region private function
    void ChangeImgColor(GameObject img, float alphaChangeR)
    {
        Color i_color = img.GetComponent<Image>().color;
        i_color.a = alphaChangeR;
        img.GetComponent<Image>().color = i_color;
    }

    void ChangeTxtColor(Text txt, float alphaChangeR)
    {
        Color t_color = txt.GetComponent<Text>().color;
        t_color.a = alphaChangeR;
        txt.GetComponent<Text>().color = t_color;
    }

    void CurIndexSetting()
    {
        if (Screen.width <= Screen.height)
            curIndex = Int32.Parse(curImgNumText_Portrait.text);
        else
            curIndex = Int32.Parse(curImgNumText_Landscape.text);
    }
    #endregion
}
