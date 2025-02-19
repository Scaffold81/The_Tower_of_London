using UnityEngine;

namespace Game.Common
{
    public class SaveSystem
    {
        public static bool HasKeyData(string saveSlot)
        {
            return PlayerPrefs.HasKey(saveSlot);
        }

        public static void SaveData(string saveSlot, string data)
        {
            PlayerPrefs.SetString(saveSlot, data);
        }

        public static string LoadDataOrDefault(string key, string defaultValue)
        {
            string loadedData = LoadData(key);
            return string.IsNullOrEmpty(loadedData) ? defaultValue : loadedData;
        }

        private static string LoadData(string saveSlot)
        {
            if (PlayerPrefs.HasKey(saveSlot))
            {
                return PlayerPrefs.GetString(saveSlot);
            }
            else
            {
                // Debug.LogError(saveSlot + " " + "PlayerPrefs.HasKey value is not valid");
                return null;
            }
        }

        public static void DeleteData(string saveSlot)
        {
            PlayerPrefs.DeleteKey(saveSlot);
        }

        public static string SaveJSON(string saveSlot, object data)
        {
            string jsonData = JsonUtility.ToJson(data);

            PlayerPrefs.SetString(saveSlot, jsonData);
            PlayerPrefs.Save();

            return jsonData;
        }

        public static T LoadJSON<T>(string saveSlot)
        {
            string jsonData = PlayerPrefs.GetString(saveSlot);

            if (!string.IsNullOrEmpty(jsonData))
            {
                T data = JsonUtility.FromJson<T>(jsonData);
                return data;
            }
            else
            {
                // Debug.LogError(saveSlot + " data not found in PlayerPrefs");
                return default(T);
            }
        }
    }
}