using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventory : MonoBehaviour
    {
        public CharacterInventory CharacterInventory;

        [ReadOnly]
        [ShowInInspector]
        private UIInventoryItem[] _inventorySlots;

        private void Start()
        {
            if (CharacterInventory != null)
            {
                _inventorySlots = GetComponentsInChildren<UIInventoryItem>();
                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            _inventorySlots.First().SlotIcon.overrideSprite = item.Icon;
        }
    }
}