using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeHome : BaseWebSite
{
    private bool alreadyClicked = false;

    public override void ResetWebSite()
    {
        
    }

    private void Awake()
    {
        
    }

    public void ButtonClicked()
    {
        if (alreadyClicked)
            return;

        alreadyClicked = true;
        RedirectToNextWebSite();
    }
}
