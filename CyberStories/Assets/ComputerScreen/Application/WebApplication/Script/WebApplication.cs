using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebApplication : BaseApplication
{
    public List<BaseWebSite> webSites;
    public List<BaseWebSite> favoriteWebSites;

    public List<FavoriteButton> favoriteButtons;

    private Dictionary<string, BaseWebSite> urlToWebSite;

    private void Awake()
    {
        foreach (var Site in webSites)
            urlToWebSite.Add(Site.Url, Site);

        for (int i = 0; i < favoriteButtons.Count; i++)
        {
            if (i < favoriteWebSites.Count)
                favoriteButtons[i].ApplyWebSite(favoriteWebSites[i]);
            else
                favoriteButtons[i].ApplyWebSite(null);
        }
    }

    public override void ResetApplication()
    {
        
    }

    public void Redirect(BaseWebSite webSite)
    {

    }
}
