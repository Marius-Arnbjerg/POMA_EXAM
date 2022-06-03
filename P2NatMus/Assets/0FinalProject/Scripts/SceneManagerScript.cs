using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string startScene, shieldInspectorScene;

    public void LoadStartScene() // These methods are being called from public events in Unity
    {
        SceneManager.LoadScene(startScene);
    }

    public void LoadShieldInspectorScene() // These methods are being called from public events in Unity
    {
        SceneManager.LoadScene(shieldInspectorScene);
    }
}
