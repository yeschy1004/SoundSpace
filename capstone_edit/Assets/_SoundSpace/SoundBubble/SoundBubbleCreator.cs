using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBubbleCreator : MonoBehaviour
{
    public GameObject soundBubblePrefab;
    public Transform currentSoundBubble;
    public int numberOfBubbles;
    RoomCreator roomCreator;
    public Transform player;
    private void Start()
    {
        roomCreator = FindObjectOfType<RoomCreator>();
    }

    public void AddBubble()
    {
        currentSoundBubble = Instantiate(soundBubblePrefab, roomCreator.currentBoundary.GetComponent<Transform>()).GetComponent<Transform>();
        currentSoundBubble.position = player.forward * 10;
        roomCreator.currentBoundary.numberOfBubbles++;
        currentSoundBubble.name = "소리방울" + roomCreator.currentBoundary.numberOfBubbles;
        currentSoundBubble.gameObject.name = currentSoundBubble.name;
    }

    public void ChangeCurrBubble(Transform soundbubble)
    {
        if (soundbubble == currentSoundBubble) return;
        if (currentSoundBubble != soundbubble)
        {
            currentSoundBubble = soundbubble;
        }
    }
    public void SetSound(AudioClip clip, Trigger.TriggerCondition condition)
    {
        currentSoundBubble.GetComponent<Trigger>().useTrigger[(int)condition] = true;
        currentSoundBubble.GetComponent<Action>().sound[(int)condition] = clip;
    }
    public void NoCurrBubble()
    {
        currentSoundBubble = null;
    }
}
