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
        }

        public void SetSavePosition(Vector2 position)
        {
            _saveData.position = position;
            SaveManager.Save(_saveData, savePath);
        }

        public Vector2 GetSavePosition()
        {
            var tmp = SaveManager.Load<JumpPowerSaveData>(savePath);
            Debug.Log(tmp);
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