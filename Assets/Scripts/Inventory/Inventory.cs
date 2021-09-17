using Assets.Scripts.InventorySystem.Items;
using Assets.Scripts.Singletons;
using System;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public Action<Item> ActionItemAdded;
        public Action<Item> ActionItemDropped;

        public int InventorySize = 20;

        public Item[] Items;

        private WorldItemManager _worldItemManager;

        private void Start()
        {
            if (WorldItemManager.instanceExists)
            {
                _worldItemManager = WorldItemManager.instance;
            }
        }

        private void Awake()
        {
            Items = new Item[InventorySize];
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