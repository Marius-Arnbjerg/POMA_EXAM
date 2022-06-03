using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source: https://forum.unity.com/threads/mobile-touch-to-orbit-pan-and-zoom-camera-without-fix-target-in-one-script.522607/

//This script should be placed on the Main Camera.
public class ObjectInspector : MonoBehaviour
{
    public Transform target; //Target for orbit and zoom

    public float startDistance = 1.75f; //Start distance from target 
    public float maxDistance = 20; //Max zoom out distance
    public float minDistance = 1f; //Max zoom in distance
    public float zoomRate = 40f; //zoom speed
    public float dampening = 5.0f; //Drag when zooming or rotating   
    public float panSpeed = 40f; //x and y rotation speed
    public int yMinLimit = -90; //y(vertical) rotation limitation
    public int yMaxLimit = 90; //y(vertical) rotation limitation

    private float _fineTuning = 0.0025f; //0.0025f allows for finetuning of zoomRate and panSpeed
    private float _xDeg = 0.0f;
    private float _yDeg = 0.0f;
    private float _currentDistance;
    private float _desiredZoomDistance;
    private Quaternion _currentRotation;
    private Quaternion _desiredRotation;
    private Quaternion _rotation;
    private Vector3 _position;

    public GameObject rotateAndZoomText;

    private void Start() //Sets the starting position of the camera
    {
        _desiredZoomDistance = startDistance;
    }

    void LateUpdate()
    {
        // If two fingers on the screen - ZOOM!
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

            _desiredZoomDistance += deltaMagDiff * Time.deltaTime * _fineTuning * zoomRate * Mathf.Abs(_desiredZoomDistance);
        } 

        // If one or two fingers on the screen and they are moving - ORBIT!
        if (Input.touchCount == 1 || (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            rotateAndZoomText.SetActive(false);
            Vector2 touchposition = Input.GetTouch(0).deltaPosition; //Gets position of touch movement
            _xDeg += touchposition.x * panSpeed * _fineTuning; //x rotation
            _yDeg -= touchposition.y * panSpeed * _fineTuning; //y rotation
            _yDeg = ClampAngle(_yDeg, yMinLimit, yMaxLimit); //Limits the y rotation
        }

        ////////Orbit
        _desiredRotation = Quaternion.Euler(_yDeg, _xDeg, 0); //0 because there's no rotation around the z axis
        _currentRotation = transform.rotation;
        _rotation = Quaternion.Lerp(_currentRotation, _desiredRotation, Time.deltaTime * dampening);
        transform.rotation = _rotation; 

        //////// Zoom
        _desiredZoomDistance = Mathf.Clamp(_desiredZoomDistance, minDistance, maxDistance);
        _currentDistance = Mathf.Lerp(_currentDistance, _desiredZoomDistance, Time.deltaTime * dampening);

        /////// Sets camera position based on zoom and orbit
        _position = target.position - (_rotation * Vector3.forward * _currentDistance);
        transform.position = _position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }
}

