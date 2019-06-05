using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingDetails : BaseDetails
{
    public int correctMail { get; set; }
    public int wrongMail { get; set; }
    public int phishingWebSite { get; set; }

    public PhishingDetails()
    {
        correctMail = 0;
        wrongMail = 0;
        phishingWebSite = 0;
    }
}
