using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    private void Awake()
    {
        levelManager = this;
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
