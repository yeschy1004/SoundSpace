using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class TempRecord : MonoBehaviour
{
    Stopwatch stopwatch;
    public Text timeleft;
    public BubbleSetter bubbleSetter;
    AudioClip record;
    AudioSource audioSource;
    string def = "00:00.00";
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject stopButton;
    [SerializeField] GameObject chooseRecord;
    float seconds;
    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        audioSource = GetComponent<AudioSource>();
        timeleft.text = def;
    }
    private void Update()
    {
        if (!Microphone.IsRecording(Microphone.devices[0]))
        {
            return;
        }
        seconds = stopwatch.ElapsedMilliseconds / 1000;
        float milsec = (stopwatch.ElapsedMilliseconds % 1000)/100;
        string time = "";
        if (seconds < 10)
        {
            time = "0";
        }
        time += seconds.ToString() + ".";
        if(milsec < 10)
        {
            time += "0";
        }
        time += milsec.ToString();
        time = "00:" + time;
        timeleft.text = time;
        if(stopwatch.ElapsedMilliseconds > 10000)
        {
            StopRecord();
        }
    }
    public void StopRecord()
    {
        stopButton.SetActive(false);
        startButton.SetActive(true);
        stopwatch.Reset();
        timeleft.text = def;
        Microphone.End(Microphone.devices[0]);
        audioSource.clip = record;
        audioSource.Play();
        chooseRecord.SetActive(true);
    }
    public void StartRecord()
    {
        stopButton.SetActive(true);
        startButton.SetActive(false);
        record = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        stopwatch.Start();
        chooseRecord.SetActive(false);
    }
    public void ConfirmRecord()
    {
        bubbleSetter.GetRecord(record, seconds);
    }
}

