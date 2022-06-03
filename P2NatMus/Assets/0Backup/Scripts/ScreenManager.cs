using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager _instance;
    public GameObject[] screens;
    public int screenState = 0;

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
        foreach (GameObject item in screens)
        {
            item.SetActive(false);
        }
        screens[screenState].SetActive(true);
    }
    public void OpenScreen(int screenNumber)
    {
        screenState = screenNumber;
        foreach (GameObject item in screens)
        {
            item.SetActive(false);
        }
        screens[screenState].SetActive(true);
    }
    
}
