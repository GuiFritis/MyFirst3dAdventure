using System.Collections.Generic;
using Padrao.Core.Singleton;

namespace Items 
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public SOInt coins;
        public SOInt energy;

        public List<ItemSetup> itemsSetup = new List<ItemSetup>();

        void Start()
        {
            Reset();
        }

        private void Reset()
        {
            foreach (var item in itemsSetup)
            {
                item.soInt.value = 0;
            }
        }

        public void AddByType(ItemType type, int amount = 1)
        {
            itemsSetup.Find(i => i.itemType == type).soInt.value += amount;
        }

        public void RemoveByType(ItemType type, int amount = 1)
        {
            if(amount < 0)
            {
                return;
            }

            var item = itemsSetup.Find(i => i.itemType == type);
            item.soInt.value -= amount;
            
            if(item.soInt.value < 0)
            {
                item.soInt.value = 0;
            }            
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;

    }
}
