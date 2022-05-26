using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpecialRoomBoundary : MonoBehaviour
{
    public static SpecialRoomBoundary specialRoom;
    bool isEnabled;
    private void Awake()
    {
        isEnabled = true;
        if(specialRoom == null)
        {
            specialRoom = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void TurnOn()
    {
        Debug.Log("turnon");
        foreach (Transform game in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (game != transform)
            {
                game.gameObject.SetActive(true);
            }
        }
    }
    public void TurnOff()
    {
        Debug.Log("turnoff");
        foreach (Transform game in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (game != transform)
            {
                game.gameObject.SetActive(false);
            }
        }
    }

}
