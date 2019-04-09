using UnityEngine;

public abstract class BaseWebSite : MonoBehaviour
{
    public string Name;
    public Sprite Icon;

    public string UrlKey;
    public string nextUrlKey;
    public bool isThereNextSite;

    public string nextUrl { get; set; }
    public string Url { get; set; }

    public bool sendMessageOnEnter = false;
    public BaseQuestManager messageDestination;

    public abstract void ResetWebSite();

    public void Load()
    {
        Url = GlobalManager.GetLocalization(UrlKey);
        if (isThereNextSite)
            nextUrl = GlobalManager.GetLocalization(nextUrlKey);

        gameObject.SetActive(false);
    }

    protected void SendMessageToQuestManager()
    {
        if (sendMessageOnEnter)
            messageDestination.GetCurrentStep().gameObject.SendMessage("OnWebSiteEnter", this);
    }
}
