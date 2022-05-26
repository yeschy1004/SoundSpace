using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Windows;

//패널 바뀔 때 마다 뒷 배경 바꾸기
//패널 바뀔 때 마다 설명 텍스트 바꾸기 =>배열로 저장


public class BackgroundChanger : MonoBehaviour
{
    public Text titleTxt;
    public Text guideTxt;
    public Sprite[] backgroundSprite;
    public Image backgroundImg;
    public string[] title
        = {"천마지의 전설", 
        "미스테리 멘션", 
        "무인도 조난, 오히려 좋아", 
        "그녀에게 맞춰라", 
        "CURSE OF \nLAKE CHEONMA"  };
    public string[] guides
        = {"안개가 자욱한 천마지의 슬픈 전설에 걸린 당신.\n운명에 맞서 저주를 풀어라!",
        "귀신에 홀린 듯 들어와버린 낡고 음침한 멘션과\n사방에서 들려오는 기분 나쁜 소리.. 어서 방을 나가야겠다.",
        "이제 무인도에 조난된지 며칠 째인지도 잘 모르겠다..\n그냥 이제 자연이 좋고.. 그냥 즐기자!^^",
        "모태솔로 외길 29년 김철수. 완벽한 소개팅녀의 진땀 빼는 질문의 정답을 맞추고 소개팅을 성공할 수 있을까?",
        "There's a tragic curse of the lake Cheonma...\nWould you be able to escape from the curse?"};

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    int curIndex;
    int preIndex;

    // Start is called before the first frame update
    void Start()
    {
        preIndex = 0; //시작은 천마지(한국어)

        CurIndexSetting();

        titleTxt.text = title[0];
        guideTxt.text = guides[0];

        backgroundImg.sprite = backgroundSprite[0];

    }

    // Update is called once per frame
    void Update()
    {
        CurIndexSetting();

        if (preIndex != curIndex)
        {
            if (curIndex != 0)
            {
                TextChanger(curIndex);
            }
            else
            {
                if (GameObject.Find("GameObject123").GetComponent<LanguageOption>().isKor)
                    TextChanger(curIndex); 
                else
                    TextChanger(4);
            }
            BackgroundImgChanger(curIndex);
            preIndex = curIndex;
        }
    }

    void TextChanger(int n)
    {
        titleTxt.text = title[n];
        guideTxt.text = guides[n];
    }

    void BackgroundImgChanger(int n)
    {
        backgroundImg.sprite = backgroundSprite[n];
    }

    void CurIndexSetting()
    {
        if (Screen.width <= Screen.height)
            curIndex = Int32.Parse(curImgNumText_Portrait.text);
        else
            curIndex = Int32.Parse(curImgNumText_Landscape.text);
    }

}
