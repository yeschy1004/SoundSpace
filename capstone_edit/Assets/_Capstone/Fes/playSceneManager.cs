using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class playSceneManager : MonoBehaviour
{
    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    int curIndex;
    bool isEng;
    int fromBackBtn = -1;


    //backbtn 오류가 있음
    // Start is called before the first frame update
    void Start()
    {
        CurIndexSetting();

        isEng = false;

        if (fromBackBtn != -1)
        {
            Debug.Log("fromBackBtn: " + fromBackBtn);
            switch (fromBackBtn)
            {
                case 0:
                    GameObject.Find("CarouselPanel_P").GetComponent<CarouselView>().GoToIndex(0);
                    GameObject.Find("CarouselPanel_LS").GetComponent<CarouselView>().GoToIndex(0);
                    break;
                case 1:
                    GameObject.Find("CarouselPanel_P").GetComponent<CarouselView>().GoToIndex(1);
                    GameObject.Find("CarouselPanel_LS").GetComponent<CarouselView>().GoToIndex(1);
                    break;
                case 2:
                    GameObject.Find("CarouselPanel_P").GetComponent<CarouselView>().GoToIndex(2);
                    GameObject.Find("CarouselPanel_LS").GetComponent<CarouselView>().GoToIndex(2);
                    break;
                case 3:
                    GameObject.Find("CarouselPanel_P").GetComponent<CarouselView>().GoToIndex(3);
                    GameObject.Find("CarouselPanel_LS").GetComponent<CarouselView>().GoToIndex(3);
                    break;
                case 4:
                    GameObject.Find("CarouselPanel_P").GetComponent<CarouselView>().GoToIndex(0);
                    GameObject.Find("CarouselPanel_LS").GetComponent<CarouselView>().GoToIndex(0);
                    break;
                default: 
                    break;
            }

            fromBackBtn = -1;
        }
    }

    public void Onclick_PlayBtn()
    {
        CurIndexSetting();

        //영어 케이스처리도 해야함
        if (curIndex == 0 && !GameObject.Find("GameObject123").GetComponent<LanguageOption>().isKor) 
            curIndex = 4;

        switch (curIndex)
        {
            case 0:
                SceneManager.LoadScene("Festival-kor");
                break;
            case 1:
                SceneManager.LoadScene("Fes1-Horror");
                break;
            case 2:
                SceneManager.LoadScene("Fes3-ASMR");
                break;
            case 3:
                SceneManager.LoadScene("Fes2-Simul");
                break;
            case 4:
                SceneManager.LoadScene("Festival-eng");
                break;
            default:
                SceneManager.LoadScene("Festival-kor");
                break;
        }

        return;
    }

    #region Onclicks
    public void Onclick_BackBtn_0()
    {
        fromBackBtn = 0;
        SceneManager.LoadScene("Main");
        
       
    }

    public void Onclick_BackBtn_1()
    {
        fromBackBtn = 1;
        SceneManager.LoadScene("Main");
        
    }

    public void Onclick_BackBtn_2()
    {
        fromBackBtn = 2;
        SceneManager.LoadScene("Main");
        
    }

    public void Onclick_BackBtn_3()
    {
        fromBackBtn = 3;
        SceneManager.LoadScene("Main");
        
    }

    public void Onclick_BackBtn_4() // 영어버전으로 수정 필요
    {
        fromBackBtn = 4;
        SceneManager.LoadScene("Main");
    }

    #endregion

    #region private function
    void CurIndexSetting()
    {
        if (Screen.width <= Screen.height)
            curIndex = Int32.Parse(curImgNumText_Portrait.text);
        else
            curIndex = Int32.Parse(curImgNumText_Landscape.text);
    }
    #endregion
}
