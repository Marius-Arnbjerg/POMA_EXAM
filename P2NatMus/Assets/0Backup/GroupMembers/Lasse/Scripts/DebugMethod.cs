using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMethod : MonoBehaviour
{
    public string debug01, debug02;
    public void DebugMethod01()
    {
        Debug.Log(debug01);
    }
    public void DebugMethod02()
    {
        Debug.Log(debug02);
    }
}
