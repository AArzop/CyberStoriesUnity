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

    public abstract void ResetWebSite();

    public void Load()
    {
        Url = GlobalManager.GetAppLocalization(UrlKey);
        if (isThereNextSite)
            nextUrl = GlobalManager.GetAppLocalization(nextUrlKey);

        gameObject.SetActive(false);
    }
}
