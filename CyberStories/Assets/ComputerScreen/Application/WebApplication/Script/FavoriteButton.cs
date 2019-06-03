using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteButton : MonoBehaviour
{
    public WebApplication app;

    public BaseWebSite Site { get; set; }

    void Awake()
    {
        ApplyWebSite(Site);
    }

    public void ApplyWebSite(BaseWebSite webSite)
    {
        Site = webSite;
        Image img = GetComponent<Image>();
        if (Site != null)
        {
            img.sprite = Site.Icon;
            Color c = Color.white;
            c.a = 1f;
            img.color = c;
        }
        else
        {
            Color c = Color.white;
            c.a = 0f;
            img.color = c;
        }
    }

    public void ClickOnFavButton()
    {
        if (Site != null)
            app.Redirect(Site);
    }
}
