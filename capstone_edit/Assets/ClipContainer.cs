using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClipContainer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static ClipContainer clipBeingDragged;
    public Image image;
    public Color color;
    Vector3 startPosition;
    public AudioClip clip;

    private void Start()
    {
        color = image.color;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        clipBeingDragged = GetComponent<ClipContainer>();
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        clipBeingDragged = null;
    }

    public void setSound()
    {
        //FindObjectOfType<SoundBubbleCreator>().SetSound(clip, condition);
    }
}
