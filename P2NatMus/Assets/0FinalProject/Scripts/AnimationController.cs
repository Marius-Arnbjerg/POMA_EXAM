using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] 
    private bool _animateOnStart = false; // A bool that is used to control if the animation should play on start.

    [SerializeField] 
    private float _delaySpeed = 1.5f; // A float that is used as a delay time

    public GameObject youFoundAnObjectInfoText; // Reference to the text gameobject

    private void Awake()
    {
        if(youFoundAnObjectInfoText != null)
            youFoundAnObjectInfoText.SetActive(false); // if the gameobject is not null, then set it to false

        _animator = GetComponent<Animator>(); 

        if (_animateOnStart)
            ResetAnimation(); // if the bool is true, then reverse the animation
    }
   
    public void PlayAnimation()
    {
        _animator.SetBool("IsOpen", true);
        StartCoroutine(StartSceneWithDelay()); // Start a coroutine that starts the inspector scene after the delay
    }
    public void EnableFoundObjectInfoText()
    {
        if(youFoundAnObjectInfoText != null)
            youFoundAnObjectInfoText.SetActive(true); // This method is activated from "TrackFoundAnimation" animation clip
    }
    IEnumerator StartSceneWithDelay()
    {
        yield return new WaitForSeconds(_delaySpeed);
        SceneManager.LoadScene("InspectorScene");
    }

    public void ResetAnimation()
    {
        _animator.SetBool("IsClose", true);
    }
}
