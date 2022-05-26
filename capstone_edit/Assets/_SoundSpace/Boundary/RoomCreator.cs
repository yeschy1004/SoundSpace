using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreator : MonoBehaviour
{
    public GameObject boundaryPrefab;
    public Boundary currentBoundary;
    public List<Boundary> boundaries = new List<Boundary>();
    public List<string> boundaryNames = new List<string>();
    public float boundaryDist;
    public Dropdown dropdown;
    public GameObject plane;
    public GameObject window;
    public InputField inputField;
    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
        AddBoundary();
        currentBoundary = boundaries[0];
        currentBoundary.gameObject.SetActive(true);
    }

    public void AddBoundary()
    {
        Boundary newBoundary = Instantiate(boundaryPrefab).GetComponent<Boundary>();
        int i = boundaries.Count + 1;
        newBoundary.name = "소리방" + i;
        newBoundary.gameObject.name = newBoundary.name;
        /*
        plane.SetActive(true);
        window.SetActive(true);*/
        boundaryNames.Add(newBoundary.name);
        newBoundary.gameObject.SetActive(false);
        boundaries.Add(newBoundary);
        dropdown.ClearOptions();
        dropdown.AddOptions(boundaryNames);
        dropdown.Hide();
    }

    public void ChangeCurrBoundary()
    {
        Boundary boundary = boundaries[dropdown.value];
        if (currentBoundary != boundary)
        {
            currentBoundary.gameObject.SetActive(false);
            currentBoundary = boundary;
            currentBoundary.gameObject.SetActive(true);    
        }
    }
    public void ChangeBGM(AudioClip clip)
    {
        currentBoundary.bgm = clip;
    }
    public void Changebackground(Sprite sprite)
    {
     //   currentBoundary.background = sprite;
    }
}
