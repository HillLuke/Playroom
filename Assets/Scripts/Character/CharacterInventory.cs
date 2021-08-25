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
        public event Action<Item> ActionItemAdded;

        public event Action<Item> ActionItemDropped;

        public int InventorySize = 20;

        public List<Item> Items = new List<Item>();

        private WorldItemManager _worldItemManager;

        private void Start()
        {
            if (WorldItemManager.instanceExists)
            {
                _worldItemManager = WorldItemManager.instance;
            }
        }

        public bool AddItem(Item item)
        {
            if (Items.Count >= InventorySize)
            {
                return false;
            }

            Items.Add(item);

            if (ActionItemAdded != null)
            {
                ActionItemAdded.Invoke(item);
            }

            return true;
        }

        public void DropFirstItem()
        {
            if (_worldItemManager != null && Items.Count > 0)
            {
                _worldItemManager.SpawnItem(Items.First(), gameObject);

                if (ActionItemAdded != null)
                {
                    ActionItemDropped.Invoke(Items.First());
                }

                Items.Remove(Items.First());
            }
        }
    }
}