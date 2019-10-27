using System;

namespace CyberStories.DataAccess
{
    public static class Level
    {
        public static string GetDescriptionByTag(string tag)
        {
            // TODO : Get these data from db
            switch (tag)
            {
                case "Level 1":
                    return "Ce premier niveau est destiné à ceux qui souhaitent aborder le thème du hameçonnage (phishing)";
                case "Level 2":
                case "Level 3":
                    return "Niveau non implémenté";
                default:
                    throw new InvalidOperationException("Tag not implemented");
            }
        }
    }
}