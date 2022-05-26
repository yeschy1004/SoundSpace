using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraText : MonoBehaviour
{
    public Transform camera1;
    public Transform camera2;
    public Text text1;
    public Text text2;

    private void Update()
    {
        text1.text = camera1.position.x + "  " + camera1.position.y + "  " + camera1.position.z + "\n" + camera1.eulerAngles.x + "  " + camera1.eulerAngles.y + "  " + camera1.eulerAngles.z;
        text2.text = camera2.position.x + "  " + camera2.position.y + "  " + camera2.position.z + "\n" + camera2.eulerAngles.x + "  " + camera2.eulerAngles.y + "  " + camera2.eulerAngles.z;
    }
}
