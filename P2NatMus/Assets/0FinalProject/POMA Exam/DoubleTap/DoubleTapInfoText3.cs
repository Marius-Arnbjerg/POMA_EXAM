using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapInfoText3 : MonoBehaviour
{
    public GameObject[] textBoxToToggle;                                                                                                                            // these textBoxToToggle gameobjects are the different information texts in the objectInspector scene

    public float touchThresh = 0.2f;                                                                                                                                //Max amount of time a touch can last for 1 to be added to _touchCounter.
    public float timeBetweenTouchesThresh = 0.3f;                                                                                                                   //Max amount of time between taps before _touchCounter resets. 


    private ScanningManager _scanningManager;                                                                                                                       //Singleton class keeping track of what objects have been scanned

    private float _touchTime;                                                                                                                                       //Keeps track of time since start of program.
    private float _timeBetweenTouches;                                                                                                                              //Keeps track of time since 1st touch ended

    private bool _isActive = false;                                                                                                                                 //Keeps track of whether the textbox is active or not.

    enum TouchState { NOTOUCH = 0, ONETOUCH = 1, DOUBLETOUCH = 2};                                                                                                  //Enumeration of possible values for touches

    private TouchState _touchCounter = TouchState.NOTOUCH;                                                                                                          //Set default value of touches to NOTOUCH (0).

    private void Start()
    {
        _scanningManager = FindObjectOfType<ScanningManager>();

        foreach (GameObject item in textBoxToToggle)                                                                                                                // Starting with setting all the textBoxToToggle gameobjects to false
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        switch (_touchCounter)
        {
            case TouchState.NOTOUCH:                                                                                                                                //If no touch detetected 
                TapOnScreen(); 
                break;

            case TouchState.ONETOUCH:                                                                                                                               //If one touch detected
                {
                    if (Time.time - _timeBetweenTouches <= timeBetweenTouchesThresh)                                                                                //and the time since end of last touch is less than timeBetweenTouchesThresh
                        TapOnScreen();                                                                                                                              //Run this method
                    else
                        _touchCounter = TouchState.NOTOUCH;                                                                                                         //If time is more than timeBetweenTouchesThresh, reset touchcount
                }
                break;

            case TouchState.DOUBLETOUCH:                
                {                 
                    _isActive = !_isActive;                                                                                                                         //_isActive should be the opposite of what it currently is 

                    textBoxToToggle[_scanningManager.currentScannedObject].SetActive(_isActive);                                                                    //Textbox is activated or deactivated according to _isActive

                    _touchCounter = TouchState.NOTOUCH;                                                                                                             //Reset touchcount
                }
                break;

            default: //Should never happen
                {
                    throw new System.Exception("unexpected touch state"); //Makes program crash
                } 
        }
    }
    private void TapOnScreen()
    {
        if (Input.touchCount == 1)                                                                                                                                  //If one finger touches the screen
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)                                                                                                        //Start of touch
            {
                _touchTime = Time.time;                                                                                                                             //Assigns _touchTime the timed value since start of program.
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended || 
                Input.GetTouch(0).phase == TouchPhase.Canceled)                                                                                                     //End of touch
            {
                _timeBetweenTouches = Time.time;

                if (Time.time - _touchTime <= touchThresh)                                                                                                          //If the time from start of touch to end of touch is less than touchThresh
                {
                    _touchCounter++;
                }
            }
        }
    }
}
