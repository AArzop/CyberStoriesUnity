using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseWebSite : MonoBehaviour
{
    public WebApplication app;

    [FormerlySerializedAs("Icon")] public Sprite icon;

    [FormerlySerializedAs("UrlKey")] public string urlKey;
    public string nextUrlKey;
    public bool isThereNextSite;

    public string NextUrl { get; set; }
    public string Url { get; set; }

    public bool sendMessageOnEnter = false;
    public BaseQuestManager messageDestination;

    public abstract void ResetWebSite();

    public void Load()
    {
        Url = GlobalManager.GetLocalization(urlKey);
        if (isThereNextSite)
            NextUrl = GlobalManager.GetLocalization(nextUrlKey);

        gameObject.SetActive(false);
    }

    public void SendMessageToQuestManager()
    {
        if (sendMessageOnEnter)
            messageDestination.gameObject.SendMessage("OnWebSiteEnter", this);
    }

    public void RedirectToNextWebSite()
    {
        if (isThereNextSite)
            app.Redirect(NextUrl);
    }
}
