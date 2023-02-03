using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Padrao.Core.Singleton;

namespace Save 
{
    public class SaveManager : Singleton<SaveManager>
    {
        public int lastLevel = 1;
        public Action<SaveSetup> OnGameLoaded;

        [SerializeField]
        private SaveSetup _saveSetup;
        private string _path;

        protected override void Awake()
        {
            base.Awake();
            _path = Application.streamingAssetsPath + "/save.txt";
            DontDestroyOnLoad(gameObject);
        }

        void Start() 
        {
            Invoke(nameof(Load), .1f);
        }

        private void CreateNewSave()
        {
            _saveSetup = new SaveSetup{
                lastLevel = 1,
                playerName = "Gui",
                checkpoint = 0,
                coins = 0,
                lifePack = 0,
                clothing = Clothing.ClothingType.NONE,
                health = 50f
            };
            if(Player.Instance != null)
            {
                _saveSetup.health = Player.Instance.health.baseHealth;
            }
        }

        #region SAVE
        private void Save()
        {
            string jsonSetup = JsonUtility.ToJson(_saveSetup);

            SaveFile(jsonSetup);
        }

        public void SaveItems()
        {
            _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.Value;
            _saveSetup.lifePack = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.Value;
        }

        public void SaveClothing()
        {
            _saveSetup.clothing = Player.Instance.GetClothingType();
        }

        public void SaveHealth()
        {
            _saveSetup.health = Player.Instance.health.GetCurHealth();
        }

        public void SaveLastLevel(int level)
        {
            _saveSetup.lastLevel = level;
            _saveSetup.checkpoint = 0;
            SaveItems();
            SaveHealth();
            Save();
        }

        public void SaveName(string name)
        {
            _saveSetup.playerName = name;
            Save();
        }

        public void SaveCheckpoint(int key)
        {
            _saveSetup.checkpoint = key;
            SaveItems();
            SaveHealth();
            Save();
        }
        #endregion

        private void SaveFile(string json)
        {
            File.WriteAllText(_path, json);
        }

        public void DeleteSave()
        {
            CreateNewSave();
            Save();
            OnGameLoaded?.Invoke(_saveSetup);
        }

        public void Load()
        {
            string fileLoaded = "";

            if(File.Exists(_path))
            {
                fileLoaded = File.ReadAllText(_path);
                _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
                lastLevel = _saveSetup.lastLevel;
            }
            else
            {
                CreateNewSave();
                Save();
            }
            OnGameLoaded?.Invoke(_saveSetup);
        }
    }

    [System.Serializable]
    public class SaveSetup
    {
        public int lastLevel;
        public string playerName;
        public int checkpoint;
        public int coins;
        public int lifePack;
        public float health;
        public Clothing.ClothingType clothing;

    }
}