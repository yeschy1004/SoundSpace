using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButtons : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image image;
    [SerializeField] Sprite enabledimage;
    [SerializeField] Sprite disabledimage;
    bool enabled;
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        ChangeTextColor();
        ChangeSprite();
    }
    public void Pressed()
    {
        enabled = !enabled;
        ChangeSprite();
        ChangeTextColor();
    }
    void ChangeTextColor()
    {
        if (enabled)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = Color.black;
        }
    }
    void ChangeSprite()
    {
        if (enabled)
        {
            image.sprite = enabledimage;
        }
        else
        {
            image.sprite = disabledimage;
        }
    }
}
