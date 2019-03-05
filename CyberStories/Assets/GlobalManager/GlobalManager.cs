
public class GlobalManager
{
    private static LocalizationManager locaManager = null;

    public static void SetupManager()
    {
        locaManager = new LocalizationManager();
    }

    public static string GetLocalization(uint locaID)
    {
        return locaManager.GetLocalization(locaID);
    }

    public void ChangeLanguage(LocalizationManager.Language language)
    {
        locaManager.ChangeLanguage(language);
    }
}
