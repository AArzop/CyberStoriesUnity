using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    public Image image;

    public void Click()
    {
        image.color = Color.blue;
    }
}
