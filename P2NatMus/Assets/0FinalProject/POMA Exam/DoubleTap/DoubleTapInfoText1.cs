using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapInfoText1 : MonoBehaviour
{
    public GameObject[] textBoxToToggle; // these textBoxToToggle gameobjects are the different information texts in the objectInspector scene

    public float touchThresh = 0.2f; //Max amount of time a touch can last for infoText to be toggled. 
    public float timeBetweenTouchesThresh = 0.3f;

    private ScanningManager _scanningManager;

    private float _touchTime; //Keeps track of time since start of program.
    private float _timeBetweenTouches;

    private bool _isActive = false;

    private int _touchCounter = 0; //This variable keeps track of whether the infoText is active(1) or not(0).


    private void Start()
    {
        _scanningManager = FindObjectOfType<ScanningManager>();

        foreach (GameObject item in textBoxToToggle) // Starting with setting all the textBoxToToggle gameobjects to false
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        switch (_touchCounter)
        {
            case 0: //If _touchCounter is 0
                TapOnScreen();
                break;

            case 1:
                {
                    if (Time.time - _timeBetweenTouches <= timeBetweenTouchesThresh)
                        TapOnScreen();
                    else
                        _touchCounter = 0;
                }
                break;

            case 2:
                {
                    _touchCounter = 0;

                    if (_isActive == false)
                    {
                        textBoxToToggle[_scanningManager.currentScannedObject].SetActive(true);
                        _isActive = true;
                    }
                    else if (_isActive == true)
                    {
                        textBoxToToggle[_scanningManager.currentScannedObject].SetActive(false);
                        _isActive = false;
                    }
                }
                break;
        }
    }

    private void TapOnScreen()
    {
        if (Input.touchCount == 1) //If one finger touches the screen
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) //Start of touch
            {
                _touchTime = Time.time; //Assigns _touchTime the timed value since start of program.
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) //End of touch
            {
                _timeBetweenTouches = Time.time;

                if (Time.time - _touchTime <= touchThresh) //If the time from start of touch to end of touch is less than touchThresh
                {
                    _touchCounter++;
                }
            }
        }
    }
}
