using UnityEngine;

namespace CyberStories.Shared.ScriptUtils
{
    public static class Prefs
    {
        private static readonly string UserEmailKey = "user_email";
        private static readonly string UserPseudoKey = "user_pseudo";

        public static string Email
        {
            get => PlayerPrefs.GetString(UserEmailKey, "");
            set
            {
                PlayerPrefs.SetString(UserEmailKey, value);
                PlayerPrefs.Save();
            }
        }

        public static string Pseudo
        {
            get => PlayerPrefs.GetString(UserPseudoKey, "");
            set
            {
                PlayerPrefs.SetString(UserPseudoKey, value);
                PlayerPrefs.Save();
            }
        }
    }
}
