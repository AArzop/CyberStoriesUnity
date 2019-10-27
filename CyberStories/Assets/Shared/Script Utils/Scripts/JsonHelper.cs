using System;
using System.Text;
using UnityEngine;

namespace CyberStories.Shared.ScriptUtils
{
    public static class JsonHelper
    {
        private static string FixJsonArray(string json)
        {
            if (json.StartsWith("["))
            {
                StringBuilder sb = new StringBuilder("{\"Items\":");
                sb.Append(json);
                sb.Append("}");
                return sb.ToString();
            }
            return json;
        }

        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(FixJsonArray(json));
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>
            {
                Items = array
            };
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
