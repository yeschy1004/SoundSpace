using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FestivalSceneChanger : MonoBehaviour
{
    public void ChangeKorScene()
    {
        SceneManager.LoadScene("Festival-kor");
    }

    public void ChangeEngScene()
    {
        SceneManager.LoadScene("Festival-eng");
    }

    public void ChangeMainScene()
    {
        SceneManager.LoadScene("Festival-main");
    }
}
