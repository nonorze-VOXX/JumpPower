using System;
using UnityEngine;

namespace Player.save
{
    public class JumpPowerSaver
    {
        private readonly string savePath = Application.persistentDataPath + "/save.json";
        [SerializeField] private JumpPowerSaveData _saveData;
        public PlayerData playerData;

        public JumpPowerSaver()
        {
            _saveData = new JumpPowerSaveData();
        }

        public JumpPowerSaver(PlayerData _playerData)
        {
            _saveData = new JumpPowerSaveData();
            playerData = _playerData;
            _playerData.savedPosition = GetSavePosition();
        }

        public void SaveGravity()
        {
            _saveData.gravityDir = playerData.gravityDirection;
            SaveManager.Save(_saveData, savePath);
        }

        public void LoadGravity()
        {
            playerData.gravityDirection = SaveManager.Load<JumpPowerSaveData>(savePath).gravityDir;
        }

        public void SaveData(Vector2 position)
        {
            SetSavePosition(position);
            SaveGravity();
        }

        public void SetSavePosition(Vector2 position)
        {
            _saveData.position = position;
            playerData.savedPosition = position;
            SaveManager.Save(_saveData, savePath);
        }

        public Vector2 GetSavePosition()
        {
            var tmp = SaveManager.Load<JumpPowerSaveData>(savePath);
            Debug.Log(tmp);
            if (tmp == null || tmp.position == Vector2.zero)
            {
                Debug.Log(tmp);
                Debug.Log(playerData);
                SetSavePosition(playerData.playerInitPosition);
            }
            else
            {
                _saveData = tmp;
            }

            return _saveData.position;
        }

        [Serializable]
        public class JumpPowerSaveData
        {
            public Vector2 position;
            public Vector2 gravityDir;

            public string SaveName()
            {
                return "JumpPowerSaveData";
            }
            //last savePoint position
        }
    }
}