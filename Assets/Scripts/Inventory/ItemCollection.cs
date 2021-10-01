using Assets.Scripts.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemCollection : MonoBehaviour
    {
        public Action<Item> ActionItemAdded;
        public Action<Item> ActionItemDropped;
        public Action<Item> ActionItemRemoved;

        public int InventorySize = 20;
        public bool CanDragIn;
        public bool CanDragOut;
        public bool CanDropItems;

        public Item[] Items;

        private void Awake()
        {
            if (Items != null)
            {
                Array.Resize(ref Items, InventorySize);
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
                return false;
            }
        }

        public void RemoveItem(int index, Item item)
        {            
            Items[index] = null;

            if (ActionItemRemoved != null)
            {
                ActionItemRemoved.Invoke(item);
            }
        }

        public void DropItem()
        {
        }
    }
}