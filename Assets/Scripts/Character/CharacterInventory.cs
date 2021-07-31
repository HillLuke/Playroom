using Assets.Scripts.Inventory.Items;
using Assets.Scripts.Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterInventory : MonoBehaviour
    {
        public event Action<Item> ActionItemAdded;

        public List<Item> Items = new List<Item>();

        private WorldItemManager _worldItemManager;

        private void Start()
        {
            if (WorldItemManager.instanceExists)
            {
                _worldItemManager = WorldItemManager.instance;
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);

            if (ActionItemAdded != null)
            {
                ActionItemAdded.Invoke(item);
            }
        }

        public void DropFirstItem()
        {
            if (_worldItemManager != null && Items.Count > 0)
            {
                _worldItemManager.SpawnItem(Items.First(), gameObject);
                Items.Remove(Items.First());

            }
        }
    }
}