using System.Collections.Generic;
using Padrao.Core.Singleton;
using UnityEngine;
using Save;

namespace Items 
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemsSetup = new List<ItemSetup>();

        void Start()
        {
            Reset();
            SaveManager.Instance.OnGameLoaded += OnLoad;
        }

        private void OnLoad(SaveSetup saveSetup)
        {
            AddByType(ItemType.COIN, saveSetup.coins);
            AddByType(ItemType.LIFE_PACK, saveSetup.lifePack);
        }

        private void Reset()
        {
            foreach (var item in itemsSetup)
            {
                item.soInt.Value = 0;
            }
        }

        public void AddByType(ItemType type, int amount = 1)
        {
            itemsSetup.Find(i => i.itemType == type).soInt.Value += amount;
        }

        public ItemSetup GetItemByType(ItemType type)
        {
            return itemsSetup.Find(i => i.itemType == type);
        }

        public void RemoveByType(ItemType type, int amount = 1)
        {
            if(amount < 0)
            {
                return;
            }

            var item = itemsSetup.Find(i => i.itemType == type);
            item.soInt.Value -= amount;

            if(item.soInt.Value < 0)
            {
                item.soInt.Value = 0;
            }            
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
        public Actions.ActionBase action;

    }
}
