using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WebApplication : BaseApplication
{
    [FormerlySerializedAs("webSites")] public List<BaseWebSite> WebSites;
    [FormerlySerializedAs("favoriteWebSites")] public List<BaseWebSite> FavoriteWebSites;

    [FormerlySerializedAs("favoriteButtons")] public List<FavoriteButton> FavoriteButtons;

    [FormerlySerializedAs("urlText")] public Text UrlText;

    private Dictionary<string, BaseWebSite> urlToWebSite;
    private BaseWebSite currentWebSite;
    private List<string> history;

    private void Awake()
    {
        urlToWebSite = new Dictionary<string, BaseWebSite>();
        history = new List<string>();

        foreach (BaseWebSite site in WebSites)
        {
            site.Load();
            urlToWebSite.Add(site.Url, site);
        }

        for (int i = 0; i < FavoriteButtons.Count; i++)
        {
            FavoriteButtons[i].ApplyWebSite(i < FavoriteWebSites.Count ? FavoriteWebSites[i] : null);
        }

        UrlText.text = "/";
    }

    public override void ResetApplication()
    {
        
    }

    public void Redirect(string url)
    {
        Redirect(urlToWebSite[url]);
    }

    public void Redirect(BaseWebSite webSite)
    {
        if (currentWebSite == webSite)
            return;

        currentWebSite?.ResetWebSite();

        currentWebSite = webSite;
        currentWebSite.ResetWebSite();
        UrlText.text = webSite.Url;
        history.Add(webSite.Url);

        DisplayWebSite();

        currentWebSite.SendMessageToQuestManager();
    }

    private void DisplayWebSite()
    {
        foreach (var site in WebSites)
            site.gameObject.SetActive(site == currentWebSite);
    }

    public void BackHistoryButton()
    {
        if (history.Count <= 1) return;
        
        // Count - 1 is the current url
        string lastUrl = history[history.Count - 2];
        history.RemoveRange(history.Count - 2, 2);

        Redirect(urlToWebSite[lastUrl]);
    }
}
