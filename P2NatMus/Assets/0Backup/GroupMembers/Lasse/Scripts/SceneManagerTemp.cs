using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTemp : MonoBehaviour
{
    private static SceneManagerTemp _instance;
    public string mainScene, arCameraScene, objectInspectorScene;
    [SerializeField]
    int currentScene = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadMainScene()
    {
        if(currentScene != 0)
        {
            currentScene = 0;
            SceneManager.LoadScene(mainScene);
        }
    }

    public void LoadARCameraScene()
    {
        if(currentScene != 1)
        {
            currentScene = 1;
            SceneManager.LoadScene(arCameraScene);
        }
    }

    public void LoadObjectInspectorScene()
    {
        Debug.Log("SCANNED");
        if(currentScene != 2)
        {
            currentScene = 2;
            SceneManager.LoadScene(objectInspectorScene);
        }
    }


    bool hasBeenActivatedWithAwesomeDelayHomie = false;
    public void ChangeSceneWithDelay()
    {
        if(!hasBeenActivatedWithAwesomeDelayHomie)
            StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
        hasBeenActivatedWithAwesomeDelayHomie = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
