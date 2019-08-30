using UnityEngine;

public abstract class BaseWebSite : MonoBehaviour
{
    public WebApplication App;

    public Sprite Icon;

    public string UrlKey;
    public string NextUrlKey;
    public bool IsThereNextSite;

    public string NextUrl { get; set; }
    public string Url { get; set; }

    public bool SendMessageOnEnter = false;
    public BaseQuestManager MessageDestination;

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
