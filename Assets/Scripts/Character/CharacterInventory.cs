using Assets.Scripts.Inventory.Items;
using Assets.Scripts.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterInventory : MonoBehaviour
    {
        public Action<Item> ActionItemAdded;
        public Action<Item> ActionItemDropped;

        public int InventorySize = 20;

        public Item[] Inventory;

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
            Inventory = new Item[InventorySize];
        }

        public bool AddItem(Item item)
        {
            int index = Array.FindIndex(Inventory, x => x == null);

            if (index != -1)
            {
                Inventory[index] = item;

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