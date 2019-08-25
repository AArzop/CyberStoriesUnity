namespace CyberStories.BusinessManagement
{
    public static class Level
    {
        public static string GetDescriptionByTag(string tag)
        {
            return DataAccess.Level.GetDescriptionByTag(tag);
        }
    }
}
