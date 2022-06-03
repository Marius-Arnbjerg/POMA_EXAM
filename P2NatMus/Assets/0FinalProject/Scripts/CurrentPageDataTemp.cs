using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPageDataTemp : MonoBehaviour
{
    private CurrentPageData _currentPageData;

    private void Start()
    {
        _currentPageData = FindObjectOfType<CurrentPageData>();
    }
    public void ChangeCurrentPageOnSceneLoad(int pageNumber) // This method is used by public events in Unity and is communicating to the currentPageData class
    {
        _currentPageData.ChangeCurrentPageOnSceneLoad(pageNumber);
    }
}
