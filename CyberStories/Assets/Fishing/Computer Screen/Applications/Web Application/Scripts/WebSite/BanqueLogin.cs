using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BanqueLogin : BaseWebSite
{
    public bool isFishingWebSite;
    public bool sendFishingMessage;

    public override void ResetWebSite()
    {
    }

    private void Awake()
    {
        GameObject go = GameObject.Find("IdentificationText");
        if (go != null)
            go.GetComponent<Text>().text = GlobalManager.GetLocalization("Loca_Identificate");

        go = GameObject.Find("LoginText");
        if (go != null)
            go.GetComponent<Text>().text = GlobalManager.GetLocalization("Loca_Email");

        go = GameObject.Find("PasswordText");
        if (go != null)
            go.GetComponent<Text>().text = GlobalManager.GetLocalization("Loca_Password");

        go = GameObject.Find("ConnexionButton");
        if (go != null)
            go.GetComponentInChildren<Text>().text = GlobalManager.GetLocalization("Loca_Connexion");
    }

    public void ConnexionButtonClicked()
    {
        if (isFishingWebSite && sendFishingMessage)
            messageDestination.SendMessage("OnPhishing", Url);

        RedirectToNextWebSite();
    }
}
