using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public GameObject onLook;
    public GameObject onTouch;
    public static IconController iconController;
    IEnumerator turnLook;
    IEnumerator turnTouch;
    private void Awake()
    {
        if (!iconController)
        {
            iconController = this;
        }
    }
    public IEnumerator TurnLook(float time)
    {
        turnLook = TurnLook(time);
        if(turnTouch != null)
            StopCoroutine(turnTouch);
        onLook.SetActive(true);
        onTouch.SetActive(false);
        yield return new WaitForSeconds(time);
        onLook.SetActive(false);
        turnLook = null;
    }
    public IEnumerator TurnTouch(float time)
    {
        turnTouch = TurnTouch(time);
        if(turnLook!= null)
            StopCoroutine(turnLook);
        Debug.Log("turntouch = " + time);
        onTouch.SetActive(true);
        onLook.SetActive(false);
        yield return new WaitForSeconds(time);
        onTouch.SetActive(false);
        turnTouch = null;
    }

}
