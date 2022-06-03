using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorScreenManager : MonoBehaviour
{
    private static InspectorScreenManager _instance;
    public GameObject[] inspectorScreens;
    public int inspectorScreenState = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        foreach (GameObject item in inspectorScreens)
        {
            item.SetActive(false);
        }
        //inspectorScreens[inspectorScreenState].SetActive(true);
    }
    public void OpenScreen(int inspectorScreenNumber)
    {
        inspectorScreenState = inspectorScreenNumber;
        foreach (GameObject item in inspectorScreens)
        {
            item.SetActive(false);
        }
        inspectorScreens[inspectorScreenState].SetActive(true);
    }
}
