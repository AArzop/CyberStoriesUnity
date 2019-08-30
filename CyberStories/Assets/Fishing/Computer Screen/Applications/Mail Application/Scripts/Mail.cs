using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Mail : MonoBehaviour
{
    #region Keys

    [FormerlySerializedAs("objectKey")] public string ObjectKey;
    [FormerlySerializedAs("sourceKey")] public string SourceKey;
    [FormerlySerializedAs("bodyKey")] public string BodyKey;
    [FormerlySerializedAs("dateTimeStr")] public string DateTimeStr;
    [FormerlySerializedAs("isThereLink")] public bool IsThereLink;
    [FormerlySerializedAs("linkKey")] public string LinkKey;

    #endregion
    
    [FormerlySerializedAs("isFishingMail")] public bool IsFishingMail;

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