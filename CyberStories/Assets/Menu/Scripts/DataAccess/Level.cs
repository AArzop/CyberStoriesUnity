using System;

namespace CyberStories.DataAccess
{
    public static class Level
    {
        public static string GetDescriptionByTag(string tag)
        {
            // TODO : Get these data from db
            if (tag == "Level 1")
                return "Yes ! First level :D";
            else if (tag == "Level 2")
                return "No ! Level 2 :(";
            else
                throw new InvalidOperationException("Tag not implemented");
        }
    }
}