using System.Collections.Generic;
using Padrao.Core.Singleton;
using UnityEngine;

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

    }
}
