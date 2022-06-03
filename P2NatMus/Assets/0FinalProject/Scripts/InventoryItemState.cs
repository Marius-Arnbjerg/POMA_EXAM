using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItemState : MonoBehaviour
{
    public int inventoryItemNumber;
    public bool hasBeenScanned = false;

    public GameObject[] icon;
    private Button _button;
    private ScanningManager _scanningManager;

    private void Start()
    {
        _button = GetComponent<Button>();
        _scanningManager = FindObjectOfType<ScanningManager>();

        if (_scanningManager.scannedObjects[inventoryItemNumber] == true) // this is a check of whether this inventory item has been scanned
            hasBeenScanned = true; 

        foreach (GameObject item in icon) // starts with setting both icon gameobjects to false
        {
            item.SetActive(false);
        }

        if(!hasBeenScanned) // this if-else sets the icon's and button's appropriate state 
        {
            icon[0].SetActive(true);
            _button.enabled = false;
        }
        else
        {
            icon[1].SetActive(true);
            _button.enabled = true;
        }
    }

}
