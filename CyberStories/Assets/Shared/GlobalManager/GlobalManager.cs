using UnityEngine.SceneManagement;

public class GlobalManager
{
    private static LocalizationManager LocaManager = null;
    public static BaseQuestManager QuestManager { get; set; }
    public static BaseDetails Details { get; set; }

    private const string MainMenuSceneName = "Menu/Menu";

    public static void SetupManager()
    {
        if (LocaManager == null)
            LocaManager = new LocalizationManager();
    }

    public static string GetLocalization(string key)
    {
        if (LocaManager != null)
            return LocaManager.GetLocalization(key);

        return "*** CANNOT OPEN FILE ***";
    }

    public void ChangeLanguage(LocalizationManager.Language language)
    {
        LocaManager.ChangeLanguage(language);
    }

    public static void ResetGlobalManager()
    {
        QuestManager = null;
    }

    public static void QuitGameToMenu(bool saveScore)
    {
        if (saveScore)
        {
        }

        QuestManager = null;
        SceneManager.LoadScene(MainMenuSceneName);
    }
}