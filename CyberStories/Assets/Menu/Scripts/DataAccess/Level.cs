using System;

namespace CyberStories.DataAccess
{
    public static class Level
    {
        public static string GetDescriptionByTag(string tag)
        {
            switch (tag)
            {
                // TODO : Get these data from db
                case "Level 1":
                    return "Yes ! First level :D";
                case "Level 2":
                    return "No ! Level 2 :(";
                default:
                    throw new InvalidOperationException("Tag not implemented");
            }
        }
    }
}