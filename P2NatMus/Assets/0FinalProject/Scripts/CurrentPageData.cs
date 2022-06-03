using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPageData : MonoBehaviour
{
    [HideInInspector]
    public int currentPage = 1; // A variable that is used to keep track of the current screen/page that is active

    private static CurrentPageData _instance;
    private void Awake() // This Awake method makes sure that this instance of the class is the only one present in the scene by using DontDestroyOnLoad and Destroy
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeCurrentPageOnSceneLoad(int pageNumber) // This method is used by the navigation bar to keep track of which page is currently active
    {
        currentPage = pageNumber;
    }
}
