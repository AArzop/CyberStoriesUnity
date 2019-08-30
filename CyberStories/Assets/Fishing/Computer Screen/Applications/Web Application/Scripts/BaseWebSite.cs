using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseWebSite : MonoBehaviour
{
    [FormerlySerializedAs("app")] public WebApplication App;

    [FormerlySerializedAs("icon")] public Sprite Icon;

    [FormerlySerializedAs("urlKey")] public string UrlKey;
    [FormerlySerializedAs("nextUrlKey")] public string NextUrlKey;
    [FormerlySerializedAs("isThereNextSite")] public bool IsThereNextSite;

    public string NextUrl { get; set; }
    public string Url { get; set; }

    [FormerlySerializedAs("sendMessageOnEnter")] public bool SendMessageOnEnter = false;
    [FormerlySerializedAs("messageDestination")] public BaseQuestManager MessageDestination;

    public abstract void ResetWebSite();

    public void Load()
    {
        Url = GlobalManager.GetLocalization(UrlKey);
        if (IsThereNextSite)
            NextUrl = GlobalManager.GetLocalization(NextUrlKey);

        gameObject.SetActive(false);
    }

    public void SendMessageToQuestManager()
    {
        if (SendMessageOnEnter)
            MessageDestination.gameObject.SendMessage("OnWebSiteEnter", this);
    }

    public void RedirectToNextWebSite()
    {
        if (IsThereNextSite)
            App.Redirect(NextUrl);
    }
}
