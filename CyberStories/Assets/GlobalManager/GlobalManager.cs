using UnityEngine.SceneManagement;

public class GlobalManager
{
    private static LocalizationManager locaManager = null;
    public static BaseQuestManager questManager { get; set; }
    public static BaseDetails details { get; set; }

    const string mainMenuSceneName = "Menu/MenuScene";

    public static void SetupManager()
    {
        if (locaManager == null)
            locaManager = new LocalizationManager();
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

    public static void QuitGameToMenu(bool saveScore)
    {
        if (saveScore)
        {

        }

        questManager = null;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
