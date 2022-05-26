using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FesSceneChanger : MonoBehaviour
{
    public static int detailNum; // 1: 천마, 2: 연애, 3: 호러, 4: ASMR, 5: Chunma 
    

    public void setD1()
    {
        detailNum = 1;
        SceneManager.LoadScene("FesDetail");
    }

    public void setD2()
    {
        detailNum = 2;
        SceneManager.LoadScene("FesDetail");
    }

    public void setD3()
    {
        detailNum = 3;
        SceneManager.LoadScene("FesDetail");
    }

    public void setD4()
    {
        detailNum = 4;
        SceneManager.LoadScene("FesDetail");
    }

    public void setD5()
    {
        detailNum = 5;
        SceneManager.LoadScene("FesDetail");
    }


    public void playBtnDecider()
    {
        if(detailNum == 1)
        {
            SceneManager.LoadScene("Festival-kor");
        }
        else if(detailNum == 2)
        {
            SceneManager.LoadScene("Fes2-Simul");
        }
        else if (detailNum == 3)
        {
            SceneManager.LoadScene("Fes1-Horror");
        }
        else if (detailNum == 4)
        {
            SceneManager.LoadScene("Fes3-ASMR");
        }
        else if (detailNum == 5)
        {
            SceneManager.LoadScene("FesMain");
        }
    }

    public void ChangeMainScene()
    {
        SceneManager.LoadScene("FesMain");
    }

    public void makeSoundSpaceScene() //수민이 씬 연결
    {
        SceneManager.LoadScene("GuestBookCreation");
    }
    public void loadSoundSpaceScene() //수민이 씬 연결
    {
        SceneManager.LoadScene("GuestBookPage");
    }

}
