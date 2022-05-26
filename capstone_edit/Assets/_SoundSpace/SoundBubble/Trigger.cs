using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    #region trigger 종류들
    //TapBeforeTime: 시간 지나기 전 tap, 시간 사용하지 않을 시 그냥 Tap
    //LookBeforeTime: 시간 지나기 전 look, 시간 사용하지 않을 시 그냥 look
    //lookAfterTime: 시간 지나고 look
    //TapAfterTime: 시간 지나고 tap
    //TimePassed: 시간 지나면 나는 고유 소리
    //Initial: 시작할 때 내고 있을 소리. 시간이 지나기 전에 나는 고유 소리
    public enum TriggerCondition { Initial, LookBeforeTime, TapBeforeTime, TimePassed, LookAfterTime, TapAfterTime, NumberOfTriggers };
    #endregion

    #region 발동 조건변수
    [Tooltip("어떤 trigger를 사용할 것인가. TimePassed가 시간 사용 조건이 된다.")]
    [SerializeField] public bool[] useTrigger = new bool[(int)TriggerCondition.NumberOfTriggers];
    [Tooltip("어떤 trigger가 cycle end 조건인가")]
    [SerializeField] public bool[] endOfCycle = new bool[(int)TriggerCondition.NumberOfTriggers];
    [Tooltip("기준이 되는 시간 > 0")]
    [SerializeField] public float time;
    #endregion

    #region 반복 횟수
    [Tooltip("반복할 횟수, 0이면 재생 한계 없음.")]
    [SerializeField] public int repeatnum;
    [Tooltip("다음 cycle을 시작하는데 드는 시간")]
    [SerializeField] public float cycleInterval;
    #endregion
    /*
    [Tooltip("다음 sequence는 cycle 몇번 후에 할꺼야?0면 안 부를꺼야")]
    [SerializeField] int untilNextSequence;
*/
    int repeatednum;
    float timer;
    bool timeUp;
    Action action;
    bool done;

    private void Awake()
    {
        action = GetComponent<Action>();
    }
    private void OnEnable()
    {
        Setup();
        repeatednum = 0;
    }

    public void Setup()
    {
        print("setup");
        timeUp = false;
        timer = 0;
        done = false;
        action.TriggerRecieve(TriggerCondition.Initial);
    }
    #region 조건확인 함수
    // Update is called once per frame
    void Update()
    {
        if (time == 0 || done) { return; }
        if (!timeUp)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timeUp = true;
                if (useTrigger[(int)TriggerCondition.TimePassed])
                {
                    action.TriggerRecieve(TriggerCondition.TimePassed);
                    if (endOfCycle[(int)TriggerCondition.TimePassed])
                    {
                        EndofCycle();
                    }
                }
            }
        }
    }
    public void LookedAt()
    {
        if (done) return;
        if (!timeUp)
        {
            if (useTrigger[(int)TriggerCondition.LookBeforeTime])
            {
                action.TriggerRecieve(TriggerCondition.LookBeforeTime);
                if (endOfCycle[(int)TriggerCondition.LookBeforeTime])
                {
                    EndofCycle();
                }
            }
        }
        else
        {
            if (useTrigger[(int)TriggerCondition.LookAfterTime])
            {
                action.TriggerRecieve(TriggerCondition.LookAfterTime);
                if (endOfCycle[(int)TriggerCondition.LookAfterTime])
                {
                    EndofCycle();
                }
            }
        }
    }
    public void Tapped()
    {
        print("tap");
        if (done) return;
        if (!timeUp)
        {
            if (useTrigger[(int)TriggerCondition.TapBeforeTime])
            {
                action.TriggerRecieve(TriggerCondition.TapBeforeTime);
                if (endOfCycle[(int)TriggerCondition.TapBeforeTime])
                {
                    EndofCycle();
                }
            }
        }
        else
        {
            if (useTrigger[(int)TriggerCondition.TapAfterTime])
            {
                action.TriggerRecieve(TriggerCondition.TapAfterTime);
                if (endOfCycle[(int)TriggerCondition.TapAfterTime])
                {
                    EndofCycle();
                }
            }
        }
    }
    #endregion

    void EndofCycle()
    {
        done = true;
        print("endofcycle");
        repeatednum++;
        if (repeatednum < repeatnum || repeatnum == 0)
        {
            Invoke("Setup", cycleInterval);
        }
        else
        {
        }
        /*
        else
        {
            //최종 노래를 불러라
            action.Invoke("CycleDoneAction", cycleInterval);
        }*/
    }

}
