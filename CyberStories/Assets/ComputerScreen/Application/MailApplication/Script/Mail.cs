using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public string ObjectKey;
    public string SourceKey;
    public string bodyKey;
    public string dateTimeStr;

    public bool isThereLink;

    public DateTime dateTime { get; set; }

    public string Object { get; set; }
    public string Source { get; set; }
    public string body { get; set; }
    public string link { get; set; }
    public string linkKey;

    private void Awake()
    {
        Object = GlobalManager.GetAppLocalization(ObjectKey);
        Source = GlobalManager.GetAppLocalization(SourceKey);
        body = GlobalManager.GetAppLocalization(bodyKey);
        if (isThereLink)
            link = GlobalManager.GetAppLocalization(linkKey);

        if (dateTimeStr != string.Empty)
            dateTime = DateTime.ParseExact(dateTimeStr, "M/d/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture);
    }
}
