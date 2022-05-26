using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBubbleEditor : MonoBehaviour
{
    GameObject collided;
    LineRenderer lineRenderer;
    [SerializeField] float linewidth = 0.1f;
    [SerializeField] float maxSightline = 100f;
    [SerializeField] GameObject CameraToCorrect;
    [SerializeField] GameObject soundBubbleSetter;
    [SerializeField] GameObject setButton;
    [SerializeField] GameObject addButton;
    [SerializeField] LayerMask layerMask;
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
        Color color = Color.green;
        Vector3 endPosition = CameraToCorrect.transform.forward * maxSightline;
        if (Physics.Raycast(CameraToCorrect.transform.position, CameraToCorrect.transform.forward, out hit, maxSightline, layerMask))
        {
            color = Color.red;
            endPosition = hit.point;
            
        }
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.SetPosition(0, CameraToCorrect.transform.position - new Vector3(0, 0.5f, 0));
        lineRenderer.SetPosition(1, endPosition);
    }

    public void AddSoundBubble()
    {
        Debug.Log("addSoundBubble");
        addButton.SetActive(false);
        setButton.SetActive(true);
        soundBubbleSetter.SetActive(true);
    }

    public void SetZero()
    {
        Quaternion lookRotationVar = Quaternion.identity;
        lookRotationVar *= Quaternion.Inverse(CameraToCorrect.transform.rotation);
        lookRotationVar *= transform.rotation;
        transform.rotation = lookRotationVar;
    }
}
