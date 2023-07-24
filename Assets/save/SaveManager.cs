using System;
using System.IO;
using UnityEngine;

namespace Player.save
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private static SaveData _saveData = new();

        private static readonly string savePath = Application.persistentDataPath + "/save.json";

        public static void SetSavePosition(Vector2 position)
        {
            _saveData.jumpPowerSaveData.position = position;
        }

        public static Vector2 GetSavePosition()
        {
            return _saveData.jumpPowerSaveData.position;
        }

        public static void Save()
        {
            var saveJson = JsonUtility.ToJson(_saveData);
            File.WriteAllText(savePath, saveJson);
        }

        public static void Load()
        {
            var saveJson = "";
            try
            {
                saveJson = File.ReadAllText(savePath);
            }
            catch (FileNotFoundException)
            {
                var f = File.Open(savePath, FileMode.Create);
                f.Close();
            }

            if (saveJson.Length == 0)
            {
                SetSavePosition(Vector2.zero);
                Save();
            }

            saveJson = File.ReadAllText(savePath);
            _saveData = JsonUtility.FromJson<SaveData>(saveJson);
        }

        [Serializable]
        public class SaveData
        {
            public JumpPowerSaveData jumpPowerSaveData;
        }

        [Serializable]
        public class JumpPowerSaveData
        {
            public Vector2 position;
            //last savePoint position
        }
    }
}