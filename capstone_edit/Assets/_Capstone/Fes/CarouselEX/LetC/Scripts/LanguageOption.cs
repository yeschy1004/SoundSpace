using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//천마지 전설 언어(한국어/영어) on off
/*언어 옵션과 관련된 모든 코드

 - 해당 PANEL 콘텐츠 swap
 - Kor/Eng 버튼 on off (색/밑줄)

*/


public class LanguageOption : MonoBehaviour
{

    public GameObject blackPanel_P;
    public GameObject blackPanel_LS;

    public GameObject languageObj_Portrait;
    public GameObject languageObj_LandscapeLeft;

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    public Text option1Text_portrait;
    public Text option2Text_portrait;
    public Text titleText_portrait;
    public Text guidesText_portrait;

    public Text option1Text_landscape;
    public Text option2Text_landscape;
    public Text titleText_landscape;
    public Text guidesText_landscape;


    public Text portrait_KorBtnTxt;
    public Text portrait_EngBtnTxt;

    public Text landscape_KorBtnTxt;
    public Text landscape_EngBtnTxt;


    public bool isKor = true;
    
    int curIndex;
    int preIndex;


    string[] optionKor
        = { "소리가 들리는 곳을 클릭하면 퍼즐을 풀어나갈 수 있습니다!",
    "열쇠가 등장하면 얻은 것이니, 열쇠를 얻기 위해 추가적인 노력을 하지 말아주세요."};
    string[] optionEng
        = { "Try tapping on the screen where you can hear a sound. You'll be able to solve the puzzle along the way.",
    "If a key appears, it means you have ontained. Please don't make any extra effort to get the key." };


    // Start is called before the first frame update
    void Start()
    {
        isKor = true;
        preIndex = 0;

        CurIndexSetting();

        SetState_LanguageObject();
        SetFont_Kor();
    }

    // Update is called once per frame
    void Update()
    {
        CurIndexSetting();

        if (preIndex != curIndex)
        {
            SetState_LanguageObject();
            preIndex = curIndex;
        }

    }

    #region Onclick Btn
    public void OnClick_KorBtn_P()
    {
        isKor = true;


        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Change_Kor_P1();
        }
        else
        {
            Change_Kor_LS1();
        }

        SetFont_Kor();


        Debug.Log("한국어 버튼");

    }

    public void OnClick_EngBtn_P()
    {
        isKor = false;


        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Change_Eng_P1();
        }
        else
        {
            Change_Eng_LS1();
        }

        SetFont_Eng();


        Debug.Log("영어 버튼");
    }

    public void OnClick_KorBtn_LS()
    {
        isKor = true;


        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Change_Kor_P2();
        }
        else
        {
            Change_Kor_LS2();
        }

        SetFont_Kor();


        Debug.Log("한국어 버튼");

    }

    public void OnClick_EngBtn_LS()
    {
        isKor = false;


        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Change_Eng_P2();
        }
        else
        {
            Change_Eng_LS2();
        }

        SetFont_Eng();


        Debug.Log("영어 버튼");
    }
    #endregion

    #region Change Discriptions

    public void Change_Kor_P1()
    {
        titleText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[0];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[0];
        option1Text_portrait.text = optionKor[0];
        option2Text_portrait.text = optionKor[1];

        titleText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[0];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[0];
        option1Text_landscape.text = optionKor[0];
        option2Text_landscape.text = optionKor[1];

    }

    public void Change_Kor_P2()
    {
        titleText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[0];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[0];
        option1Text_portrait.text = optionKor[0];
        option2Text_portrait.text = optionKor[1];

        titleText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[0];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[0];
        option1Text_landscape.text = optionKor[0];
        option2Text_landscape.text = optionKor[1];

    }

    public void Change_Kor_LS1()
    {
        titleText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[0];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[0];
        option1Text_landscape.text = optionKor[0];
        option2Text_landscape.text = optionKor[1];

        titleText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[0];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[0];
        option1Text_portrait.text = optionKor[0];
        option2Text_portrait.text = optionKor[1];
    }

    public void Change_Kor_LS2()
    {
        titleText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[0];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[0];
        option1Text_landscape.text = optionKor[0];
        option2Text_landscape.text = optionKor[1];

        titleText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[0];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[0];
        option1Text_portrait.text = optionKor[0];
        option2Text_portrait.text = optionKor[1];
    }

    public void Change_Eng_P1()
    {
        titleText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[4];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[4];
        option1Text_portrait.text = optionEng[0];
        option2Text_portrait.text = optionEng[1];

        titleText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[4];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[4];
        option1Text_landscape.text = optionEng[0];
        option2Text_landscape.text = optionEng[1];
    }

    public void Change_Eng_P2()
    {
        titleText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[4];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[4];
        option1Text_portrait.text = optionEng[0];
        option2Text_portrait.text = optionEng[1];

        titleText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[4];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[4];
        option1Text_landscape.text = optionEng[0];
        option2Text_landscape.text = optionEng[1];
    }

    public void Change_Eng_LS1()
    {
        titleText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[4];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[4];
        option1Text_landscape.text = optionEng[0];
        option2Text_landscape.text = optionEng[1];

        titleText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().title[4];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_LS").GetComponent<BackgroundChanger>().guides[4];
        option1Text_portrait.text = optionEng[0];
        option2Text_portrait.text = optionEng[1];
    }

    public void Change_Eng_LS2()
    {
        titleText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[4];
        guidesText_landscape.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[4];
        option1Text_landscape.text = optionEng[0];
        option2Text_landscape.text = optionEng[1];

        titleText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().title[4];
        guidesText_portrait.text = GameObject.Find("CarouselPanel_P").GetComponent<BackgroundChanger>().guides[4];
        option1Text_portrait.text = optionEng[0];
        option2Text_portrait.text = optionEng[1];
    }
    #endregion


    #region private function
    void SetState_LanguageObject()
    {
        if (curIndex == 0)
        {
            languageObj_Portrait.SetActive(true);
            languageObj_LandscapeLeft.SetActive(true);
        }
            
        else
        {
            languageObj_Portrait.SetActive(false);
            languageObj_LandscapeLeft.SetActive(false);
        }
            

    }

    void SetFont_Kor()
    {
        portrait_KorBtnTxt.text = "<b><color=#ffffff>KOR</color></b>";
        landscape_KorBtnTxt.text = "<b><color=#ffffff>KOR</color></b>";

        portrait_EngBtnTxt.text = "<color=#727272>ENG</color>";
        landscape_EngBtnTxt.text = "<color=#727272>ENG</color>";

    }

    void SetFont_Eng()
    {
        portrait_EngBtnTxt.color = new Color(255, 255, 255);
        portrait_EngBtnTxt.text = "<b><color=#ffffff>ENG</color></b>";

        landscape_EngBtnTxt.color = new Color(255, 255, 255);
        landscape_EngBtnTxt.text = "<b><color=#ffffff>ENG</color></b>";

        portrait_KorBtnTxt.color = new Color(115, 115, 115);
        portrait_KorBtnTxt.text = "<color=#727272>KOR</color>";

        landscape_KorBtnTxt.color = new Color(115, 115, 115);
        landscape_KorBtnTxt.text = "<color=#727272>KOR</color>";
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
