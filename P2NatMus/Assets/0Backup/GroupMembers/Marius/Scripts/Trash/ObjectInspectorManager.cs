using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInspectorManager : MonoBehaviour
{
    public GameObject rune, horn, shield;
    public void SetRuneActive()
    {
        rune.SetActive(true);
    }
}
