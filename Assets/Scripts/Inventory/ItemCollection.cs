using Assets.Scripts.Inventory.Items;
using System;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemCollection : MonoBehaviour
    {
        public Action<Item> ActionItemAdded;
        public Action<Item> ActionItemDropped;

        public int InventorySize = 20;

        public Item[] DefaultItems;
        public Item[] Items;

        private void Start()
        {
        }

        private void Awake()
        {
            Items = new Item[InventorySize];

            if (DefaultItems != null && DefaultItems.Length <= Items.Length)
            {
                for (int i = 0; i < DefaultItems.Length; i++)
                {
                    Items[i] = DefaultItems[i];
                }
            }
        }

        public bool AddItem(Item item)
        {
            int index = Array.FindIndex(Items, x => x == null);

            if (index != -1)
            {
                Items[index] = item;

                if (ActionItemAdded != null)
                {
                    ActionItemAdded.Invoke(item);
                }

                return true;
            }
            else
            {
                //TODO feedback to player they cant pickup
                return false;
            }
        }

        public void DropItem()
        {
        }

        //public void DropFirstItem()
        //{
        //    if (_worldItemManager != null && Inventory.Count > 0)
        //    {
        //        _worldItemManager.SpawnItem(Inventory.First(), gameObject);

        //        if (ActionItemAdded != null)
        //        {
        //            ActionItemDropped.Invoke(Inventory.First());
        //        }

        //        Inventory.Remove(Inventory.First());
        //    }
        //}
    }
}