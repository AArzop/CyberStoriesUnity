
public class GlobalManager
{
    private static LocalizationManager locaManager = null;
    public static bool isLoading = false;

    public static void SetupManager()
    {
        locaManager = new LocalizationManager();
        isLoading = true;
    }

    public static string GetLocalization(uint locaID)
    {
        return locaManager.GetLocalization(locaID);
    }

    public static string GetAppLocalization(string key)
    {
        if (locaManager == null)
            return "***";
        return locaManager.GetAppLocalization(key);
    }

    public void ChangeLanguage(LocalizationManager.Language language)
    {
        locaManager.ChangeLanguage(language);
    }
}
