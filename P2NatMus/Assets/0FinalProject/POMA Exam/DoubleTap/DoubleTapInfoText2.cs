using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapInfoText2 : MonoBehaviour
{
    public GameObject[] textBoxToToggle; // these textBoxToToggle gameobjects are the different information texts in the objectInspector scene

    public float touchThresh = 0.2f; //Max amount of time a touch can last for infoText to be toggled. 
    public float timeBetweenTouchesThresh = 0.3f;

    private ScanningManager _scanningManager;

    private float _touchTime; //Keeps track of time since start of program.
    private float _timeBetweenTouches;

    private bool _isActive = false;

    private int _touchCounter = 0;

    private const int _NO_TOUCH = 0;
    private const int _ONE_TOUCH = 1;
    private const int _DOUBLE_TOUCH = 2;



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
            case _NO_TOUCH: //If _touchCounter is 0
                TapOnScreen();
                break;

            case _ONE_TOUCH:
                {
                    if (Time.time - _timeBetweenTouches <= timeBetweenTouchesThresh)
                        TapOnScreen();
                    else
                        _touchCounter = _NO_TOUCH;
                }
                break;

            case _DOUBLE_TOUCH:
                {
                    _touchCounter = _NO_TOUCH;

                    if (_isActive == false)
                    {
                        textBoxToToggle[_scanningManager.currentScannedObject].SetActive(true);
                    }
                    else
                    {
                        textBoxToToggle[_scanningManager.currentScannedObject].SetActive(false);
                    }
                    _isActive = !_isActive; //_isActive should be the opposite of what it currently is 
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
