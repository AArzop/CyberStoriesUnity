using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArnaqueLogin : BaseWebSite
{
    public override void ResetWebSite()
    {
    }

    private void Awake()
    {
        
    }

    public void OnClickedButton()
    {
        messageDestination.SendMessage("OnPhishing", Url);
        RedirectToNextWebSite();
    }
}
