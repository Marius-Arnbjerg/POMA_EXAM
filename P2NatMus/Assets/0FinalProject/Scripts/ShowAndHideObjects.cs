using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHideObjects : MonoBehaviour
{
    private ScanningManager _scanningManager;
    public GameObject[] objects;

    private void Start()
    {
        _scanningManager = FindObjectOfType<ScanningManager>();

        foreach (GameObject item in objects) // Starts with setting all the gameobjects in the array to false
        {
            item.SetActive(false);
        }
        objects[_scanningManager.currentScannedObject].SetActive(true); // Sets the appropriate scanned object to true
    }
}
