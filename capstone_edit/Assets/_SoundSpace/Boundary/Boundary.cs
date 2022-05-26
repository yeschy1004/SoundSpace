using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boundary : MonoBehaviour {
    public AudioClip bgm;
    public Texture[] backgrounds;
    public int numberOfBubbles;
    public Material skymat;
    private void OnEnable()
    {
        if (bgm)
        {
            BGM BGm = FindObjectOfType<BGM>();
            if (BGm)
            {
                print("bgm");
                BGm.ChangeBGM(bgm);
            }
        }
        string[] SkyboxTextures = { "_UpTex", "_DownTex", "_FrontTex", "_BackTex", "_LeftTex", "_RightTex" };
        for (int i = 0; i < 6; i++)
        {
            if (backgrounds[i] != null)
            {
                skymat.SetTexture(SkyboxTextures[i], backgrounds[i]);
            }
        }
    }
    private void Start()
    {
        RenderSettings.skybox = skymat;
        numberOfBubbles = 0;
    }
    public void Fadeout(GameObject warper)
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.Equals(warper)) continue;
            child.gameObject.SetActive(false);
        }
    }
}
