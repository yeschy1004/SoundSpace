using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interactor : MonoBehaviour
{
    public RectTransform touchPulse;
    GameObject collided;
    AudioSource audioSource;
    LineRenderer lineRenderer;
    [SerializeField] float linewidth = 0.1f;
    [SerializeField] float maxSightline = 100f;
    [SerializeField] GameObject CameraToCorrect;
    [SerializeField] RectTransform canvas;
    [SerializeField] CanvasScaler scaler;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = linewidth;
        lineRenderer.endWidth = linewidth;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Color color = Color.red;
        Vector3 endPosition = CameraToCorrect.transform.forward * maxSightline;
        if (Physics.Raycast(CameraToCorrect.transform.position, CameraToCorrect.transform.forward, out hit, maxSightline))
        {
            color = Color.green;
            endPosition = hit.point; Trigger trigger;
            if ((trigger = hit.transform.gameObject.GetComponent<Trigger>()) != null)
            {
                //보고 있는 것이 달라졌을 때만 lookedat 발동
                if (collided != hit.transform.gameObject)
                {
                    NotLooking();
                    collided = hit.transform.gameObject;
                    trigger.LookedAt();
                }
                //보고 있을 때 tap하면 tapped 발동
                if (Input.GetButtonDown("Fire1"))
                {
                    Vector2 pos;
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), null, out pos)) ;
                    touchPulse.localPosition = pos;
                    touchPulse.gameObject.GetComponent<Animator>().SetTrigger("Touch");
                    trigger.Tapped();
                }
            }
        }
        else
        {
            NotLooking();
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("pushed");
                Vector2 pos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), null, out pos)) ;
                touchPulse.localPosition = pos;
                touchPulse.gameObject.GetComponent<Animator>().SetTrigger("Touch");
            }
        }
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        lineRenderer.SetPosition(0, CameraToCorrect.transform.position); // - new Vector3(0,0.5f,0)
        lineRenderer.SetPosition(1, endPosition);
    }
    
    void NotLooking()
    {
        if (collided == null) return;
        Action action = collided.GetComponent<Action>();
        if(action != null)
        {
            action.notLooking();
        }
        collided = null;
    }
    public void Reset()
    {
        /*
        SetZero();
        SetZero();
        */
    }
    public void SetZero()
    {
        Quaternion lookRotationVar = Quaternion.identity;
        lookRotationVar *= Quaternion.Inverse(CameraToCorrect.transform.rotation);
        lookRotationVar *= transform.rotation;
        transform.rotation = lookRotationVar;
    }
}
