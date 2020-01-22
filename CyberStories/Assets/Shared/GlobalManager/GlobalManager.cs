using UnityEngine.SceneManagement;

public class GlobalManager
{
    private static LocalizationManager locaManager = null;
    public static BaseQuestManager questManager { get; set; }
    public static BaseDetails details { get; set; }

    const string mainMenuSceneName = "Menu/MenuScene";

    private class PlayerInformation
    {
        public PlayerInformation(string pseudo, string mail)
        {
            this.pseudo = pseudo;
            this.mail = mail;
        }

        public string pseudo;
        public string mail;
    }
    private PlayerInformation playerInfo;

    public enum Level
    {
        Phishing,
        Other
    };

    public static void RegisterScore(Level level, float Score)
    {
        // Do some stuff
    }

    public void SetPlayerInformation(string pseudo, string mail)
    {
        playerInfo = new PlayerInformation(pseudo, mail);
    }

    public static void SetupManager()
    {
        if (locaManager == null)
            locaManager = new LocalizationManager();
    }

    public static string GetLocalization(string key)
    {
        if (locaManager == null)
            locaManager = new LocalizationManager();
        return locaManager.GetLocalization(key);
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

    public static void AddNewLocalization(string key, string value)
    {
        if (locaManager == null)
            locaManager = new LocalizationManager();

        locaManager.AddNewLocalization(key, value);
    }
}
