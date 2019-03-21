using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public string Object;
    public string Source;
    public string body;
    public DateTime dateTime;

    private void Start()
    {
        dateTime = DateTime.Now;
    }
}
