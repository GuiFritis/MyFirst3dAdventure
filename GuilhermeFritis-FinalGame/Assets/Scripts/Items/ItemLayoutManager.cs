using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itemLayouts = new List<ItemLayout>();

        void Start()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            foreach (var item in ItemManager.Instance.itemsSetup)
            {
                var setup = Instantiate(prefabLayout, container);
                setup.Load(item);
                itemLayouts.Add(setup);
            }
        }
    }
}
