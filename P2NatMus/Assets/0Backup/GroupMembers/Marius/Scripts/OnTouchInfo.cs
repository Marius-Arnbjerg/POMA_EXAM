using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tis script should be placed on the canvas in ObjectInspectorScene

public class OnTouchInfo : MonoBehaviour
{
    public GameObject infoText; //Text that 
    
    private float _touchCounter = 0; //This variable keeps track of whether the infoText is active(1) or not(0).
    private float _touchTime; //Keeps track of time since start of program.

    public float touchThresh = 0.2f; //Max amount of time a touch can last for infoText to toggled. 

    private void Awake()
    {
        infoText.SetActive(false);
    }

    private void Update()
    {
        if(_touchCounter == 0)
        {
            SetInfoTextActive();
        }
        else if (_touchCounter == 1)
        {
            InfoTextInactive();
        }
    }

    private void SetInfoTextActive()
    {
        if (Input.touchCount == 1) //If one finger touches the screen
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) //Start of touch
            {
                _touchTime = Time.time; //Assigns _touchTime the timed value since start of program.
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) //End of touch
            {
                if (Time.time - _touchTime <= touchThresh) //If the time from start of touch to end of touch is less than touchThresh
                {
                    infoText.SetActive(true);
                    _touchCounter++; //Adds 1 to _touchCounter to indicate that infoText is active
                }
            }
        }
    }

    private void InfoTextInactive()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchTime = Time.time;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                if (Time.time - _touchTime <= touchThresh)
                {
                    infoText.SetActive(false);
                    _touchCounter = 0; //Resets _touchCounter so the process can be repeated.
                }
            }
        }
    }
}
