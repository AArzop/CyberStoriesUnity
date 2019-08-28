using System.Collections.Generic;
using UnityEngine.UI;

public class WebApplication : BaseApplication
{
    public List<BaseWebSite> webSites;
    public List<BaseWebSite> favoriteWebSites;

    public List<FavoriteButton> favoriteButtons;

    public Text urlText;

    private Dictionary<string, BaseWebSite> urlToWebSite;
    private BaseWebSite currentWebSite;
    private List<string> history;

    private void Awake()
    {
        urlToWebSite = new Dictionary<string, BaseWebSite>();
        history = new List<string>();

        foreach (var site in webSites)
        {
            site.Load();
            urlToWebSite.Add(site.Url, site);
        }

        for (int i = 0; i < favoriteButtons.Count; i++)
        {
            favoriteButtons[i].ApplyWebSite(i < favoriteWebSites.Count ? favoriteWebSites[i] : null);
        }

        urlText.text = "/";
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
        urlText.text = webSite.Url;
        history.Add(webSite.Url);

        DisplayWebSite();

        currentWebSite.SendMessageToQuestManager();
    }

    private void DisplayWebSite()
    {
        foreach (var site in webSites)
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
