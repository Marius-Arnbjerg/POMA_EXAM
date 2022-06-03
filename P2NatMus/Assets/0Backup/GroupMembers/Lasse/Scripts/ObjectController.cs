using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    // Assign in the inspector
    public GameObject objectToRotate;
    public Camera camera;
    public Slider sliderRotate;
    public Slider sliderZoom;


    // Preserve the original and current orientation
    private float previousValue;

    void Awake()
    {
        // Assign a callback for when this slider changes
        sliderRotate.onValueChanged.AddListener(OnSliderChangedRotate);
        sliderZoom.onValueChanged.AddListener(OnSliderChangedZoom);

        // And current value
        previousValue = sliderRotate.value;
    }

    void OnSliderChangedRotate(float value)
    {
        // How much we've changed
        float delta = value - previousValue;
        objectToRotate.transform.Rotate(Vector3.up * delta * 360);

        // Set our previous value for the next change
        previousValue = value;
    }
    void OnSliderChangedZoom(float value)
    {
        // How much we've changed
        float delta = value - previousValue;
        camera.fieldOfView -= delta * 20;

        // Set our previous value for the next change
        previousValue = value;
    }
}
