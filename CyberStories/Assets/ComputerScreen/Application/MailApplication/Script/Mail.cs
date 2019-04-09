﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    #region Keys
    public string ObjectKey;
    public string SourceKey;
    public string bodyKey;
    public string dateTimeStr;
    public bool isThereLink;
    public string linkKey;
    #endregion

    public bool isPhishingMail;

    public DateTime dateTime { get; set; }
    public string Object { get; set; }
    public string Source { get; set; }
    public string body { get; set; }
    public string link { get; set; }

    private void Awake()
    {
        Object = GlobalManager.GetLocalization(ObjectKey);
        Source = GlobalManager.GetLocalization(SourceKey);
        body = GlobalManager.GetLocalization(bodyKey);
        if (isThereLink)
            link = GlobalManager.GetLocalization(linkKey);

        if (dateTimeStr != string.Empty)
            dateTime = DateTime.ParseExact(dateTimeStr, "M/d/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture);
    }
}
