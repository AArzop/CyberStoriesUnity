using System;
using UnityEngine;

public class Mail : MonoBehaviour
{
    #region Keys

    public string ObjectKey;
    public string SourceKey;
    public string BodyKey;
    public string DateTimeStr;
    public bool IsThereLink;
    public string LinkKey;

    #endregion
    
    public bool IsFishingMail;

    public DateTime DateTime { get; set; }
    public string Object { get; set; }
    public string Source { get; set; }
    public string Body { get; set; }
    public string Link { get; set; }

    private void Awake()
    {
        Object = GlobalManager.GetLocalization(ObjectKey);
        Source = GlobalManager.GetLocalization(SourceKey);
        Body = GlobalManager.GetLocalization(BodyKey);
        if (IsThereLink)
            Link = GlobalManager.GetLocalization(LinkKey);

        if (DateTimeStr != string.Empty)
            DateTime = DateTime.ParseExact(DateTimeStr, "M/d/yyyy hh:mm",
                System.Globalization.CultureInfo.InvariantCulture);
    }
}