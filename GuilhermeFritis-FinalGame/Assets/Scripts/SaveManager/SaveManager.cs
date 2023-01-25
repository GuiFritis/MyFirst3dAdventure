using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Padrao.Core.Singleton;

namespace Save 
{
    public class SaveManager : Singleton<SaveManager>
    {
        private SaveSetup _saveSetup;

        protected override void Awake()
        {
            _saveSetup = new SaveSetup{
                lastLevel = 0,
                playerName = "Gui"
            };
            DontDestroyOnLoad(gameObject);
        }

        #region SAVE
        private void Save()
        {
            string jsonSetup = JsonUtility.ToJson(_saveSetup);

            SaveFile(jsonSetup);
        }

        public void SaveLastLevel(int level)
        {
            _saveSetup.lastLevel = level;
            Save();
        }

        public void SaveName(string name)
        {
            _saveSetup.playerName = name;
            Save();
        }
        #endregion

        private void SaveFile(string json)
        {
            string path = Application.persistentDataPath + "/save.txt";

            File.WriteAllText(path, json);
        }
    }

    [System.Serializable]
    public class SaveSetup
    {
        public int lastLevel;
        public string playerName;
    }
}