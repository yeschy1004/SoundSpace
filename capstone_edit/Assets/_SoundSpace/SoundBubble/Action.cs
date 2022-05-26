using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    #region 소리들
    [SerializeField] public AudioClip[] sound = new AudioClip[(int)Trigger.TriggerCondition.NumberOfTriggers];
    [Tooltip("0라면 소리 길이 만큼")]
    [SerializeField] public float[] soundTime = new float[(int)Trigger.TriggerCondition.NumberOfTriggers];
    [SerializeField] public Transform[] warpdest = new Transform[(int)Trigger.TriggerCondition.NumberOfTriggers];
    [SerializeField] public GameObject[] activateObject = new GameObject[(int)Trigger.TriggerCondition.NumberOfTriggers];
    [SerializeField] public bool[] switchOn = new bool[(int)Trigger.TriggerCondition.NumberOfTriggers];
    #endregion
    [Tooltip("trigger 재생 후 default sound 재생?")]
    public bool turnDefaultSound;
    /*
    [Tooltip("다시 default sound를 재생할 때도 방해금지를 설정하자.")]
    public bool useDefaultTime;*/
    [Tooltip("볼때만 소리를 내어라")]
    [SerializeField] public bool soundOnlyWhenLooked;
    AudioSource audioSource;
    Trigger trigger;
    bool inAction;
    Trigger.TriggerCondition defaultSound;
    #region forEditmode
    public float multiplication;
    public Transform material;
    public GameObject mesh;
    public GameObject minDistance;
    public GameObject maxDistance;
    public Color[] colors;
   public void AdjustTexture()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = GetComponent<AudioSource>().minDistance*multiplication;
        collider.radius = GetComponent<AudioSource>().minDistance*multiplication;
        if(material!= null)
            material.localScale = new Vector3(collider.radius, collider.radius, collider.radius);

    }
    #endregion
    private void Awake()
    {
        if (mesh.activeInHierarchy)
        {
            mesh.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
        }
        minDistance.SetActive(false);
        maxDistance.SetActive(false);
        SphereCollider collider = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        collider.radius = audioSource.minDistance;//* transform.localScale.x;
        trigger = GetComponent<Trigger>();
        inAction = false;
    }
    private void OnEnable()
    {
        inAction = false;
    }
    public void TriggerRecieve(Trigger.TriggerCondition condition)
    {
        if (inAction) return;
        if (IconController.iconController != null)
        {
            switch (condition)
            {
                case Trigger.TriggerCondition.LookAfterTime:
                case Trigger.TriggerCondition.LookBeforeTime:
                    IconController.iconController.StopCoroutine("TurnTouch");
                    IconController.iconController.StartCoroutine("TurnLook", sound[(int)condition].length);
                    break;
                case Trigger.TriggerCondition.TapAfterTime:
                case Trigger.TriggerCondition.TapBeforeTime:
                    IconController.iconController.StopCoroutine("TurnTouch");
                    IconController.iconController.StartCoroutine("TurnTouch", sound[(int)condition].length);
                    break;
            }
        }
        if (warpdest[(int)condition] != null)
        {
            print("callwarp");
            StartCoroutine(Warp(warpdest[(int)condition], soundTime[(int)condition], sound[(int)condition]));
        }
        else if(activateObject[(int)condition] != null)
        {
            ActivateObject(activateObject[(int)condition], switchOn[(int)condition]);
            ActivateSound(sound[(int)condition], soundTime[(int)condition]);
        }
        else
        {
            print(condition);
            ActivateSound(sound[(int)condition], soundTime[(int)condition]);
        }
    }
    public void notLooking()
    {
        if (!soundOnlyWhenLooked) { return; }
        if (audioSource.clip == sound[(int)Trigger.TriggerCondition.LookAfterTime]|| audioSource.clip == sound[(int)Trigger.TriggerCondition.LookBeforeTime])
        {
            audioSource.Stop();
        }
        IconController.iconController.onLook.SetActive(false);
    }
    private void Update()
    {
        if (!turnDefaultSound) { return; }
        if (audioSource.isPlaying) {
            return; }
        if (!sound[(int)defaultSound]) { return; }
        /*
        float time = useDefaultTime ? soundTime[(int)defaultSound] : 0;*/
        ActivateSound(sound[(int)defaultSound], 0);
    }
    void ActivateSound(AudioClip clip, float time)
    {
        print("activate");
        inAction = true;
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
            time = 0;
        }
        Invoke("ActionEnd", time);
    }
    void ActivateObject(GameObject activation, bool turnOn)
    {
        activation.SetActive(turnOn);
    }
    void ActionEnd()
    {
        inAction = false;
    }
    /*last는 나중에 시퀀스가 생기면 없어질거 같으므로 아예 쓰지 않을 예정*
    public void CycleDoneAction()
    {
        if (last != null)
        {
            audioSource.loop = true;
            audioSource.clip = last;
            audioSource.Play();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }*/
    IEnumerator Warp(Transform destination, float time, AudioClip clip)
    {
        print("warp");
        inAction = true;
        time = time == 0 ? clip.length : time;
        if(clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else {
            audioSource.Stop();
            time = 0;
        }
        Boundary home = GetComponentInParent<Boundary>();
        transform.parent = null;
        home.gameObject.SetActive(false);
        //home.Fadeout(gameObject);
        //print(time);
        yield return new WaitForSeconds(time);
        GameObject.Find("Player").transform.position = destination.position;
        destination.gameObject.SetActive(true);
        transform.parent = home.GetComponent<Transform>();
        //home.gameObject.SetActive(false);
    }
    
}
