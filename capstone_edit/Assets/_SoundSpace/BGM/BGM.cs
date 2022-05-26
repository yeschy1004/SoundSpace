using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    AudioSource audioSource;
    [SerializeField] float fadeTime = 1f;//fade에 걸리는 시간
    [SerializeField] float audioSourceVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceVolume = audioSource.volume;
    }
    public void ChangeBGM(AudioClip clip)
    {
        if (clip != audioSource.clip)
        {
            StartCoroutine("FadeOutIn", clip);
        }
    }

    IEnumerator FadeOutIn(AudioClip clip)
    {
        while(audioSource.volume > 0)
        {
            audioSource.volume -= audioSourceVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.clip = clip;
        audioSource.Play();
        while(audioSource.volume < audioSourceVolume)
        {
            audioSource.volume += audioSourceVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
