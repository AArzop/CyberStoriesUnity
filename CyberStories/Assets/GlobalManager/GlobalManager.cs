
public class GlobalManager
{
    private static LocalizationManager locaManager = null;
    public static BaseQuestManager questManager { get; set; }

    public static void SetupManager()
    {
        locaManager = new LocalizationManager();
        questManager = null;
    }

    public static string GetLocalization(string key)
    {
        if (locaManager != null)
            return locaManager.GetLocalization(key);

        return "*** CANNOT OPEN FILE ***";
    }

    public void ChangeLanguage(LocalizationManager.Language language)
    {
        locaManager.ChangeLanguage(language);
    }

    public static void ResetGlobalManager()
    {
        questManager = null;
    }
}
