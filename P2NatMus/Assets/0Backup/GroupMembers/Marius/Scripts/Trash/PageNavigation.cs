using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Source: https://pressstart.vip/tutorials/2019/05/15/95/swiping-pages-in-unity.html

//This script should be placed on 'Panel'

public class PageNavigation : MonoBehaviour, IDragHandler, IEndDragHandler //These two classes uses built in functions to detect when a touch is dragged and when that drag has ended.
{


    public Vector3 panelLocationVector; //Stores location of the panel

    public float percentThreshold = 0.2f; //Threshold that decides how large a swipe has to be to change screen.
    public float easing = 0.4f;

    public int totalPages = 3;
    private int _currentPage = 1; //Keeps track of the page that is currently visible



    void Start()
    {
        panelLocationVector = transform.position; //Sets location of the panel to its current location, this is the start posiion of the panel
    }


    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;    
        transform.position = panelLocationVector - new Vector3(difference, 0, 0); 
    }


    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width; 
                                                                                        
        if (Mathf.Abs(percentage) >= percentThreshold) 
        {
            Vector3 newLocation = panelLocationVector; 

            if (percentage > 0 && _currentPage < totalPages) 
            {
                _currentPage++; //Add one to _currentPage
                newLocation += new Vector3(-Screen.width, 0, 0); 
            }
            else if (percentage < 0 && _currentPage > 1) 
            {
                _currentPage--; //Minus one to _currentPage
                newLocation += new Vector3(Screen.width, 0, 0); //And set new panelLocation = Change screen
            }

            StartCoroutine(SmoothMove(transform.position, newLocation, easing)); //Smoothens the transition between screens

            panelLocationVector = newLocation; //Assigns the panelLocation the new screen location
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocationVector, easing)); 
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds) //IEnumerator that smoothens swipes
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }


    //THE METHODS BELOW HAS TO DO WITH NAVIGATION USING THE BOTTOMBAR

    public Image homeButton, cameraButton, inventoryButton; //The images connected to buttons in bottombar should be assigned accordingly

    public GameObject PanelLocationObject; //The 'Panel' should be assigned to this Gameobject.
                                           //This gameobject is neccesarry for the panels location to directly, visually, change when using the bottombar. 


    public void OnClickHomeBottom()
    {
        Vector3 homeScreen = new Vector3(720, 1480, 0); //New vector3 based on the homeScreens location
        PanelLocationObject.transform.position = homeScreen; //new Vector3(720, 1480, 0);
        panelLocationVector = homeScreen; //new Vector3(720, 1480, 0);

        //HomeScreen is equal to _currentPage 1,
        if (_currentPage == 2) //so if the _currentPage is equal to 2(CameraScreen) 
        {
            _currentPage--;
        }
        else if (_currentPage == 3) //or 3(InventoryScreen),
        {
            _currentPage = 3 - 2; //then the _currentPage variable should change based on direction of swipe.
        }
    }

    public void OnClickCameraBottom() //Should be placed on Camera button in bottombar
    {
        Vector3 cameraScreen = new Vector3(-720, 1480, 0); //New vector3 based on the cameraScreens location
        PanelLocationObject.transform.position = cameraScreen;//new Vector3(-720, 1480, 0);
        panelLocationVector = cameraScreen; //new Vector3(-720, 1480, 0);

        if (_currentPage == 1)
        {
            _currentPage++;
        }
        else if (_currentPage == 3)
        {
            _currentPage--;
        }
    }

    public void OnClickInventoryBottom() //Should be placed on Inventory button in bottombar
    {
        Vector3 inventoryScreen = new Vector3(-2160, 1480, 0); //New vector3 based on the inventoryScreens location
        PanelLocationObject.transform.position = inventoryScreen;//new Vector3(-2160, 1480, 0);
        panelLocationVector = inventoryScreen;//new Vector3(-2160, 1480, 0);

        if (_currentPage == 1)
        {
            _currentPage = 1 + 2;
        }
        else if (_currentPage == 2)
        {
            _currentPage++;
        }
    }

    public void SetTransparencyCameraButton(float transparency)
    {
        Color color = cameraButton.color;
        color.a = transparency;
        cameraButton.color = color;
    }

    public void SetTransparencyHomeButton(float transparency)
    {
        Color color = homeButton.color;
        color.a = transparency;
        homeButton.color = color;
    }

    public void SetTransparencyInventoryButton(float transparency)
    {
        Color color = inventoryButton.color;
        color.a = transparency;
        inventoryButton.color = color;
    }

    private void Update()
    {
        if (panelLocationVector == new Vector3(720, 1480, 0)) //Transparency of bottom home button
            SetTransparencyHomeButton(1f);
        else
            SetTransparencyHomeButton(0.5f);

        if (panelLocationVector == new Vector3(-720, 1480, 0)) //Transparency of bottom camera button
            SetTransparencyCameraButton(1f);
        else
            SetTransparencyCameraButton(0.5f);

        if (panelLocationVector == new Vector3(-2160, 1480, 0)) //Transparency of bottom inventory button
            SetTransparencyInventoryButton(1f);
        else
            SetTransparencyInventoryButton(0.5f);
    }
}