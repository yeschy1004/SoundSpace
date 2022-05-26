using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSetter : MonoBehaviour
{
    [SerializeField]Material[]possible;
    [SerializeField] GameObject soundBubblePrefab;
    [SerializeField] GameObject setButton;
    [SerializeField] GameObject addButton;
    [SerializeField] GameObject recordPanel;
    [SerializeField] Text text;
    SphereCollider sphereCollider;
    MeshRenderer meshRenderer;
    Action action;
    public bool placeable;
    int number = 0;

    // Start is called before the first frame update
    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = possible[0];
        placeable = true;
        number = PlayerPrefs.GetInt("numberOfVoice");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!placeable) return;
        meshRenderer.material = possible[1];
        placeable = false;
    }
    private void OnTriggerExit(Collider other)
    {
        meshRenderer.material = possible[0];
        placeable = true;
    }
    public void SetSoundBubble()
    {
        if (!placeable) return;
        GameObject soundBubble = Instantiate(soundBubblePrefab, transform.position, Quaternion.identity, SpecialRoomBoundary.specialRoom.transform);
        action = soundBubble.GetComponent<Action>();
        action.mesh.SetActive(true);
        action.mesh.GetComponent < MeshRenderer>().material.color = action.colors[Random.Range(0, action.colors.Length)];
        recordPanel.SetActive(true);
        setButton.SetActive(false);
    }
    public void GetRecord(AudioClip audioClip, float seconds)
    {
        number++;
        PlayerPrefs.SetInt("numberOfVoice", number);
        string tempPath = Path.Combine(Application.persistentDataPath, "Audio");
        tempPath = Path.Combine(tempPath, number.ToString() + ".wav");
        text.text = tempPath;
        SavWav.Save(tempPath, audioClip);
        recordPanel.SetActive(false);
        action.sound[(int)Trigger.TriggerCondition.TapBeforeTime] = audioClip;
        action.soundTime[(int)Trigger.TriggerCondition.TapBeforeTime] = seconds;
        addButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
