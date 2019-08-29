using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager
{
    public enum Language
    {
        Francais = 1,
        English = 2
    };

    #region Attributes

    private Language currentLanguage;
    private readonly Dictionary<string, string> locaDictionary;
    private const string FileName = "loca.csv";
    private bool loadSuccessfull = false;

    #endregion

    public LocalizationManager()
    {
        currentLanguage = Language.Francais;
        locaDictionary = new Dictionary<string, string>();
        string path = Path.Combine(Application.dataPath + "/Shared/GlobalManager/LocalizationManager", FileName);
        if (!File.Exists(path))
        {
            loadSuccessfull = false;
            Debug.Assert(false, "Loca.csv is missing");
            return;
        }

        LoadLocalizationFile();
    }

    private void LoadLocalizationFile()
    {
        locaDictionary.Clear();
        string path = Path.Combine(Application.dataPath + "/Shared/GlobalManager/LocalizationManager", FileName);

        string fileData = File.ReadAllText(path);
        string[] lines = fileData.Split("\n"[0]);

        for (uint id = 1; id < lines.Length - 1; ++id)
        {
            lines[id] = lines[id].Replace('~', '\n');
            System.Console.WriteLine(lines[id]);
            string[] loc = lines[id].Split(";"[0]);

            locaDictionary.Add(loc[0], loc[(int) currentLanguage]);
        }

        loadSuccessfull = true;
    }

    public void ChangeLanguage(Language language)
    {
        currentLanguage = language;
        LoadLocalizationFile();
    }

    public string GetLocalization(string key)
    {
        if (locaDictionary.ContainsKey(key))
            return locaDictionary[key];

        return "*** LOCA NOT FOUND ****";
    }
}