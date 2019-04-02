
public class GlobalManager
{
    private static LocalizationManager locaManager = null;
    public static bool isLoading = false;

    public static void SetupManager()
    {
        locaManager = new LocalizationManager();
        isLoading = true;
    }

    public static string GetLocalization(string key)
    {
        if (locaManager != null)
            return locaManager.GetLocalization(key);

        return "*** ERROR ***";
    }

    public void ChangeLanguage(LocalizationManager.Language language)
    {
        locaManager.ChangeLanguage(language);
    }
}
