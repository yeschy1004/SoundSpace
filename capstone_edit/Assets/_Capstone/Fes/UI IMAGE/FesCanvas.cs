﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FesCanvas : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width * 10 / 19, true);
    }

    // Update is called once per frame
    void Update()
    {
        Screen.SetResolution(Screen.width, Screen.width * 10 / 19, true);
    }
}
