using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Source: https://pressstart.vip/tutorials/2019/05/15/95/swiping-pages-in-unity.html

//This script should be placed on 'Panel'

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler //These two classes uses built in functions to detect when a touch is dragged and when that drag has ended.
{
    public Vector3 panelLocationVector; //Stores location of the panel

    public float percentThreshold = 0.2f; //Threshold that decides how large a swipe has to be to change screen.
    public float easing = 0.4f; 

    public int totalPages = 3;
    private int _currentPage = 1; //Keeps track of the page that is currently visible

    CurrentPageData _currentPageData;
    private void Awake()
    {
        //_currentPageData = FindObjectOfType<CurrentPageData>();
    }

    void Start()
    {
        panelLocationVector = transform.position; //Sets location of the panel to its current location
        _currentPageData = FindObjectOfType<CurrentPageData>();

        if (_currentPageData.currentPage == 1)
        {
            OnClickHomeBottom();
        }
        else if (_currentPageData.currentPage == 2)
        {
            OnClickCameraBottom();
        }
        else if (_currentPageData.currentPage == 3)
        {
            OnClickInventoryBottom();
        }
        else
            OnClickHomeBottom();
    }


    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x; //'difference' gets the x position values from start of drag to end of drag to see how far we've dragged. 
        transform.position = panelLocationVector - new Vector3(difference, 0, 0); //Subtracts the difference of how far we've dragged from the panels idle state.
    }


    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width; //percentage variable that gets x values of the swipe to see direction of the swipe,
                                                                                    //and divides it with screen width to see how much of the screen was swiped across. 

        if (Mathf.Abs(percentage) >= percentThreshold) //If the 'percentage' variable is larger than percentThreshhold 
        {
            Vector3 newLocation = panelLocationVector; //Create newLocation Vector3 variable that stores PanelLocation 

            if (percentage > 0 && _currentPage < totalPages) //If percentage is positive (x values of swipe above 0) and the _currentpage nr. is smaller than the amount of totalPages
            {
                _currentPage++; //Add one to _currentPage
                newLocation += new Vector3(-Screen.width, 0, 0); //And set new panelLocation = Change screen
            }
            else if (percentage < 0 && _currentPage > 1) //If percentage variable is negative (x values of swipe below 0) and current page above 1
            {
                _currentPage--; //Minus one to _currentPage
                newLocation += new Vector3(Screen.width, 0, 0); //And set new panelLocation = Change screen
            }

            StartCoroutine(SmoothMove(transform.position, newLocation, easing)); //Smoothens the transition between screens

            panelLocationVector = newLocation; //Assigns the panelLocation the new screen location
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocationVector, easing)); //If 'percentage' is smaller than percentThreshold, smoothen transition back to current panelLocation. 
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

    public Color highLighted = new Color(); // This color is used for the navigation bar
    public Color notHighLighted = new Color(); // This color is used for the navigation bar

    Vector3 homeScreen = new Vector3(720, 1480, 0); //New vector3 based on the homeScreens location
    Vector3 cameraScreen = new Vector3(-720, 1480, 0); //New vector3 based on the cameraScreens location
    Vector3 inventoryScreen = new Vector3(-2160, 1480, 0); //New vector3 based on the inventoryScreens location

    public void OnClickHomeBottom() //Should be placed on Home button in bottombar
    {
        
        PanelLocationObject.transform.position = homeScreen; //new Vector3(720, 1480, 0);
        panelLocationVector = homeScreen; //new Vector3(720, 1480, 0);

        //HomeScreen is equal to _currentPage 1,
        if (_currentPage == 2) //so if the _currentPage is equal to 2(CameraScreen) 
        {
            _currentPage--; 
        }
        else if(_currentPage == 3) //or 3(InventoryScreen),
        {
            _currentPage = _currentPage - 2; //then the _currentPage variable should change based on direction of swipe.
        }
        _currentPageData.currentPage = 1;
    }

    public void OnClickCameraBottom() //Should be placed on Camera button in bottombar
    {
        
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
        _currentPageData.currentPage = 2;
    }

    public void OnClickInventoryBottom() //Should be placed on Inventory button in bottombar
    {
        
        PanelLocationObject.transform.position = inventoryScreen;//new Vector3(-2160, 1480, 0);
        panelLocationVector = inventoryScreen;//new Vector3(-2160, 1480, 0);

        if (_currentPage == 1)
        {            
            _currentPage = _currentPage + 2;
        }
        else if (_currentPage == 2)
        {
            _currentPage++;
        }
        _currentPageData.currentPage = 3;
    }

    public void SetColorCameraButton(Color color)
    {
        Color _color;
        _color = color;
        cameraButton.color = _color;
    }

    public void SetColorHomeButton(Color color)
    {
        Color _color;
        _color = color;
        homeButton.color = _color;
    }

    public void SetColorInventoryButton(Color color)
    {
        Color _color;
        _color = color;
        inventoryButton.color = _color;
    }


    private void Update()
    {
        if (panelLocationVector == new Vector3(720, 1480, 0)) //Color of bottom home button
            SetColorHomeButton(highLighted);
        else
            SetColorHomeButton(notHighLighted);

        if (panelLocationVector == new Vector3(-720, 1480, 0)) //Color of bottom camera button
            SetColorCameraButton(highLighted);
        else
            SetColorCameraButton(notHighLighted);

        if (panelLocationVector == new Vector3(-2160, 1480, 0)) //Color of bottom inventory button
            SetColorInventoryButton(highLighted);
        else
            SetColorInventoryButton(notHighLighted);
    }
}