using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SoundBubbleGizmo : MonoBehaviour {
    AudioSource audioSource;
    public Transform minDistance;
    public Transform maxDistance;
    SphereCollider sphereCollider;

    // Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        sphereCollider.radius = audioSource.minDistance;
        minDistance.localScale = new Vector3(audioSource.minDistance * 2f, audioSource.minDistance * 2f, audioSource.minDistance * 2f);
        maxDistance.localScale = new Vector3(audioSource.maxDistance * 2f, audioSource.maxDistance * 2f, audioSource.maxDistance * 2f);
        /*
        if (Application.isEditor)
        {
            minDistance.gameObject.SetActive(false);
            maxDistance.gameObject.SetActive(false);
            enabled = false;
        }
        else
        {
            minDistance.gameObject.SetActive(true);
            maxDistance.gameObject.SetActive(true);
            enabled = true;
        }*/
    }
}
