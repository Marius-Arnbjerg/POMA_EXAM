using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private int _currentScannedObject;
    private ScanningManager _scanningManager;

    private void Start()
    {
        _scanningManager = FindObjectOfType<ScanningManager>();
    }
    public void GetScannedObjectNumber(int objectNumber) // When an image is scanned, this method is called and the appropriate 
    {                                                   // object number is being passed through and passed on to the scanningManager
        _currentScannedObject = objectNumber;
        _scanningManager.GetCurrentScannedObject(_currentScannedObject);
    }
}
