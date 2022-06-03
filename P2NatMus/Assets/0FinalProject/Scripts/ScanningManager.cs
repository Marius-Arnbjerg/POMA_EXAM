using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningManager : MonoBehaviour 
{
    public int currentScannedObject;
    public bool[] scannedObjects;

    private static ScanningManager _instance;

    private void Awake()                        // This Awake method makes sure that this instance of the 
    {                                          // class is the only one present in the scene by using DontDestroyOnLoad and Destroy
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void GetCurrentScannedObject(int objectNumber) // This method gets information of what objects has been scanned 
    {                                                     // and then adds that information to the scannedObjects array. 
        currentScannedObject = objectNumber;
        scannedObjects[currentScannedObject] = true;
    }
}
