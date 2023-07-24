using System;
using UnityEngine;

namespace Player.save
{
    public class JumpPowerSaver : MonoBehaviour
    {
        [SerializeField] private static JumpPowerSaveData _saveData;

        private static readonly string savePath = Application.persistentDataPath + "/save.json";
        public static PlayerData playerData;

        static JumpPowerSaver()
        {
            _saveData = new JumpPowerSaveData();
        }

        public static void SetSavePosition(Vector2 position)
        {
            _saveData.position = position;
            SaveManager.Save(_saveData, savePath);
        }

        public static Vector2 GetSavePosition()
        {
            var tmp = SaveManager.Load<JumpPowerSaveData>(savePath);
            if (tmp == null)
                SetSavePosition(playerData.playerInitPosition);
            else
                _saveData = tmp;

            return _saveData.position;
        }

        [Serializable]
        public class JumpPowerSaveData
        {
            public Vector2 position;

            public JumpPowerSaveData()
            {
                position = Vector2.zero;
            }
            //last savePoint position
        }
    }
}