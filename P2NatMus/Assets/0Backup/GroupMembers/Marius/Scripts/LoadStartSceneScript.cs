using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartSceneScript : MonoBehaviour
{
    public string startScene;
    public void OnLoadStartScene()
    {
        SceneManager.LoadScene(startScene);
    }
}