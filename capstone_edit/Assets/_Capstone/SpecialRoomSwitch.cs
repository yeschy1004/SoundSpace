using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomSwitch : MonoBehaviour
{
    public bool turnOn;
    // Start is called before the first frame update
    void Start()
    {
        SpecialRoomBoundary specialRoom = FindObjectOfType<SpecialRoomBoundary>();
        if(specialRoom != null)
        {
            Debug.Log("turnOn = " + turnOn);
            if (turnOn)
            {
                specialRoom.TurnOn();
            }
            else
            {
                specialRoom.TurnOff();
            }
        }
        else
        {
            Debug.Log("no");
        }
    }


}
