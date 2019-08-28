using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Mail : MonoBehaviour
{
    #region Keys

    [FormerlySerializedAs("ObjectKey")] public string objectKey;
    [FormerlySerializedAs("SourceKey")] public string sourceKey;
    public string bodyKey;
    public string dateTimeStr;
    public bool isThereLink;
    public string linkKey;

    #endregion

    [FormerlySerializedAs("isPhishingMail")]
    public bool isFishingMail;

    public DateTime DateTime { get; set; }
    public string Object { get; set; }
    public string Source { get; set; }
    public string Body { get; set; }
    public string Link { get; set; }

    private void Awake()
    {
        Object = GlobalManager.GetLocalization(objectKey);
        Source = GlobalManager.GetLocalization(sourceKey);
        Body = GlobalManager.GetLocalization(bodyKey);
        if (isThereLink)
            Link = GlobalManager.GetLocalization(linkKey);

        if (dateTimeStr != string.Empty)
            DateTime = DateTime.ParseExact(dateTimeStr, "M/d/yyyy hh:mm",
                System.Globalization.CultureInfo.InvariantCulture);
    }
}