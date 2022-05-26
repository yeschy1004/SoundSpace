using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protractor : MonoBehaviour
{
    public Slider zmover;
    public Transform soundBubble;
    public Transform player;
//    public Transform AreaVector;

    public void ChangeZ()
    {
        soundBubble.LookAt(Vector3.zero);
        soundBubble.position += transform.forward;
    }
}
