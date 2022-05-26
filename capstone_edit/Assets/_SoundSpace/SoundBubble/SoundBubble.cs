using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBubble : MonoBehaviour
{
    Trigger trigger;
    Action action;

    public string name;
    private void Awake()
    {
        trigger = GetComponent<Trigger>();
        action = GetComponent<Action>();
    }
    public void SetTriggerUse(bool[]usage)
    {
        for(int i = 0; i < (int)Trigger.TriggerCondition.NumberOfTriggers; i++)
        {
            trigger.useTrigger[i] = usage[i];
        }
    }
}