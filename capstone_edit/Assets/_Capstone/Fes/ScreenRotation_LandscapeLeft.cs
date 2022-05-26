using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotation_LandscapeLeft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        Invoke("CheckScreenLandscapeLeft", 2.0f);
    }

    void CheckScreenLandscapeLeft()
    {
        if (Screen.orientation != ScreenOrientation.Landscape)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

}
