using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarManager : MonoBehaviour
{
    Image image;

    float transparency;


    // Start is called before the first frame update
    public void SetTransparency()
    {
        if (image != null)
        {
            UnityEngine.Color __alpha = image.color;
            __alpha.a = transparency;
            image.color = __alpha;
        }
    }
}
