using System.Collections;
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
    private Dictionary<string, string> locaDictionnary;
    private const string fileName = "loca.csv";
    private const string innerPath = "/Shared/GlobalManager/LocalizationManager";
    private bool loadSuccessfull = false;

    #endregion

    public LocalizationManager()
    {
        currentLanguage = Language.Francais;
        locaDictionnary = new Dictionary<string, string>();
        string path = Path.Combine(Application.dataPath + innerPath, fileName);
        Debug.Log(path);
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
        locaDictionnary.Clear();
        string path = Path.Combine(Application.dataPath + innerPath, fileName);

        string fileData = File.ReadAllText(path);
        string[] lines = fileData.Split("\n"[0]);

        for (uint id = 1; id < lines.Length - 1; ++id)
        {
            lines[id] = lines[id].Replace('~', '\n');
            System.Console.WriteLine(lines[id]);
            string[] loc = lines[id].Split(";"[0]);

            locaDictionnary.Add(loc[0], loc[(int)currentLanguage]);
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
        if (locaDictionnary.ContainsKey(key))
            return locaDictionnary[key];

        return "*** UNFOUND LOCA ****";
    }

    public void AddNewLocalization(string key, string value)
    {
        locaDictionnary[key] = value;
    }
}
