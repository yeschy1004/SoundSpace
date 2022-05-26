using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FesDetails : MonoBehaviour
{
    public int curNum;
    public GameObject d1Panel;
    public GameObject d2Panel;
    public GameObject d3Panel;
    public GameObject d4Panel;
    public GameObject d5Panel;

    // Start is called before the first frame update
    void Start()
    {
        d1Panel.gameObject.SetActive(false);
        d2Panel.gameObject.SetActive(false);
        d3Panel.gameObject.SetActive(false);
        d4Panel.gameObject.SetActive(false);
        d5Panel.gameObject.SetActive(false);

        print(FesSceneChanger.detailNum);
        curNum = FesSceneChanger.detailNum;

        if(curNum == 1)
        {
            d1Panel.gameObject.SetActive(true);
        }else if(curNum == 2)
        {
            d2Panel.gameObject.SetActive(true);
        }
        else if (curNum == 3)
        {
            d3Panel.gameObject.SetActive(true);
        }
        else if (curNum == 4)
        {
            d4Panel.gameObject.SetActive(true);
        }
        else if (curNum == 5)
        {
            d5Panel.gameObject.SetActive(true);
        }

    }

}
