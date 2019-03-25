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
    public string linkKey;

    public DateTime dateTime { get; set; }

    public string Object { get; set; }
    public string Source { get; set; }
    public string body { get; set; }
    public string link { get; set; }

    private void Start()
    {
        
        Object = GlobalManager.GetMailLocalization(ObjectKey);
        Source = GlobalManager.GetMailLocalization(SourceKey);
        body = GlobalManager.GetMailLocalization(bodyKey);

        if (isThereLink)
            link = GlobalManager.GetMailLocalization(linkKey);

        if (dateTimeStr != string.Empty)
            dateTime = DateTime.ParseExact(dateTimeStr, "M/d/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture);
            
    }
}
