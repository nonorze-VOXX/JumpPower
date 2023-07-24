using System.IO;
using UnityEngine;

namespace Player.save
{
    public class SaveManager : MonoBehaviour
    {
        public static void Save<T>(T saveData, string path)
        {
            var saveJson = JsonUtility.ToJson(saveData);
            File.WriteAllText(path, saveJson);
        }

        public static T Load<T>(string path) where T : new()
        {
            var saveJson = "";
            try
            {
                saveJson = File.ReadAllText(path);
            }
            catch (FileNotFoundException)
            {
                var f = File.Open(path, FileMode.Create);
                f.Close();
                Save(new T(), path);
            }

            return JsonUtility.FromJson<T>(saveJson);
        }
    }
}