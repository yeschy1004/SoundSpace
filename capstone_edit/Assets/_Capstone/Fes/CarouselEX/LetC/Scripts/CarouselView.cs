using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Carousel view 구현
// 콘텐츠 수 4개(eng +1)

public class CarouselView : MonoBehaviour {


    public RectTransform[] images;
    public RectTransform view_window;
    public int imageSize;

    private bool  canSwipe;
    private float image_width;
    private float lerpTimer;
    private float lerpPosition;
    private float mousePositionStartX;
    private float mousePositionEndX;
    private float dragAmount;
    private float screenPosition;
    private float lastScreenPosition;

    /// Space between images.
    public  float image_gap = 30;

    public int swipeThrustHold = 30;
    [HideInInspector]

    /// The index of the current image on display.
    public int current_index;

    public Text curImgNumText_Portrait;
    public Text curImgNumText_Landscape;

    #region mono
    // Use this for initialization
    void Start () {
        image_width = view_window.rect.width;
        for (int i = 1; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector2(((image_width + image_gap) * i), 0);
        }
    }

    // Update is called once per frame
    void Update () {

        curImgNumText_Portrait.text = current_index.ToString();
        curImgNumText_Landscape.text = current_index.ToString();

        lerpTimer = lerpTimer + Time.deltaTime;

        if (lerpTimer < 0.333f)
        {
            screenPosition = Mathf.Lerp(lastScreenPosition, lerpPosition * -1, lerpTimer * 3);
            lastScreenPosition = screenPosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            canSwipe = true;
            mousePositionStartX = Input.mousePosition.x;
        }


        if (Input.GetMouseButton(0))
        {
            if (canSwipe)
            {
                mousePositionEndX = Input.mousePosition.x;
                dragAmount = mousePositionEndX - mousePositionStartX;
                screenPosition = lastScreenPosition + dragAmount;
            }
        }

        if (Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe)
        {
            canSwipe = false;
            lastScreenPosition = screenPosition;
            if (current_index < images.Length)
                OnSwipeComplete();

            else if (current_index == images.Length && dragAmount < 0)
                lerpTimer = 0;

            else if (current_index == images.Length && dragAmount > 0)
                OnSwipeComplete();
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector2(screenPosition + ((image_width + image_gap) * i), 0);
        }
    }
    #endregion


    #region private methods
    void OnSwipeComplete()
    {
        lastScreenPosition = screenPosition;

        if (dragAmount > 0)
        {
            if (dragAmount >= swipeThrustHold)
            {
                if (current_index == 0)
                {
                    lerpTimer = 0; lerpPosition = 0;
                }
                else
                {
                    current_index--;
                    lerpTimer = 0;
                    if (current_index < 0)
                        current_index = 0;
                    lerpPosition = (image_width + image_gap) * current_index;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }
        else if (dragAmount < 0)
        {
            if (Mathf.Abs(dragAmount) >= swipeThrustHold)
            {
                if (current_index == images.Length-1)
                {
                    lerpTimer = 0;
                    lerpPosition = (image_width + image_gap) * current_index;
                }
                else
                {
                    lerpTimer = 0;
                    current_index++;
                    lerpPosition = (image_width + image_gap) * current_index;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }
        dragAmount = 0;
    }
    #endregion



    #region public methods
    public void GoToIndex(int value)
    {
        current_index = value;
        lerpTimer = 0;
        lerpPosition = (image_width + image_gap) * current_index;
        screenPosition = lerpPosition * -1;
        lastScreenPosition = screenPosition;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector2(screenPosition + ((image_width + image_gap) * i), 0);
        }
    }

    public void GoToIndexSmooth(int value)
    {
        current_index = value;
        lerpTimer = 0;
        lerpPosition = (image_width + image_gap) * current_index;
    }
    #endregion
}
