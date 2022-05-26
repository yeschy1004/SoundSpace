using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//패널 위치 조정(선택: -250, 선택x : -275 정도?)
//다른 패널 투명도 설정

public class PreNextPanel_LandscapeLeft_Animation : MonoBehaviour
{
    public GameObject carouselObj;

    public GameObject[] images_object;
    public GameObject[] images_box;
    
    public GameObject[] images_thumbnail;

    public GameObject[] images_guide1;
    public GameObject[] images_guide2;
    public Text[] texts_guide1;
    public Text[] texts_guide2;

    public Text[] texts_title;
    public Text[] texts_discription;

    //젤 나중에
    public Text texts_kor;
    public Text texts_eng;

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    public int maxImgSize; // 작성 필요

    int defaultIndex = 0;

    int curIndex;
    int preIndex;

    int positionY_default = 0;
    int positionY_changedRange = 380;


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
            curIndex = Int32.Parse(curImgNumText_Portrait.text); 
            defaultIndex = 0;
        }
        else //가로 모드
        {
            if (isPortrait_initiate) // 세로 -> 가로 전환
            {
                curIndex = Int32.Parse(curImgNumText_Portrait.text);
                defaultIndex = curIndex;
            }
            else
            {
                curIndex = Int32.Parse(curImgNumText_Landscape.text);
                defaultIndex = 0;
            }   
        }

        preIndex = defaultIndex;   //시작 콘텐츠

        Vector3 carouselObjPst = carouselObj.transform.localPosition;
        carouselObjPst.x = 0;
        carouselObjPst.y = positionY_default;
        carouselObj.transform.localPosition = carouselObjPst;

        Debug.Log("가로 모드 앞뒤패널 start");
       

        for (int i=0; i<maxImgSize; i++)
        {
            if(i!= defaultIndex)
            {
                Vector3 pos = images_object[i].transform.localPosition;
                pos.x = 0;
                pos.y = -positionY_changedRange;
                images_object[i].transform.localPosition = pos;

                alpha_changedRange = alpha_nonSelected;
            }
            else
            {
                alpha_changedRange = alpha_selected;
            }

            // 투명도 조절
            ChangeImgColor(images_thumbnail[i], alpha_changedRange);
            ChangeImgColor(images_box[i], alpha_changedRange);
            ChangeImgColor(images_guide1[i], alpha_changedRange);
            ChangeImgColor(images_guide2[i], alpha_changedRange);

            ChangeTxtColor(texts_guide1[i], alpha_changedRange);
            ChangeTxtColor(texts_guide2[i], alpha_changedRange);
            ChangeTxtColor(texts_title[i], alpha_changedRange);
            ChangeTxtColor(texts_discription[i], alpha_changedRange);
        }


    }

    void Update()
    {
        CurIndexSetting();

        if (preIndex != curIndex)
        {
            Vector3 pos1 = images_object[preIndex].transform.localPosition;
            pos1.x = 0;
            pos1.y = -positionY_changedRange;
            images_object[preIndex].transform.localPosition = pos1;

            Vector3 pos2 = images_object[curIndex].transform.localPosition;
            pos2.x = 0;
            pos2.y = 0;
            images_object[curIndex].transform.localPosition = pos2;

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
                ChangeImgColor(images_box[i], alpha_changedRange);
                ChangeImgColor(images_guide1[i], alpha_changedRange);
                ChangeImgColor(images_guide2[i], alpha_changedRange);

                ChangeTxtColor(texts_guide1[i], alpha_changedRange);
                ChangeTxtColor(texts_guide2[i], alpha_changedRange);
                ChangeTxtColor(texts_title[i], alpha_changedRange);
                ChangeTxtColor(texts_discription[i], alpha_changedRange);


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
