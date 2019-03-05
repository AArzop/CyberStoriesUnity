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
    private Dictionary<uint, string> locaDictionnary;
    private const string fileName = "loca.csv";
    private bool loadSuccessfull = false;

    #endregion

    public LocalizationManager()
    {
        currentLanguage = Language.Francais;
        locaDictionnary = new Dictionary<uint, string>();
        string path = Path.Combine(Application.dataPath + "/GlobalManager/LocalizationManager", fileName);
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
        string path = Path.Combine(Application.dataPath + "/GlobalManager/LocalizationManager", fileName);

        string fileData = File.ReadAllText(path);
        string[] lines = fileData.Split("\n"[0]);

        for (uint id = 1; id < lines.Length - 1; ++id)
        {
            string[] loc = lines[id].Split(";"[0]);

            locaDictionnary.Add(id, loc[(int)currentLanguage]);
        }
        loadSuccessfull = true;
    }

    public void ChangeLanguage(Language language)
    {
        currentLanguage = language;
        LoadLocalizationFile();
    }

    public string GetLocalization(uint locaID)
    {
        if (loadSuccessfull)
            return locaDictionnary[locaID];

        return "*** LOCA ERROR ****";
    }
}
