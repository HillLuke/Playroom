using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventory : UIBase
    {
        public CharacterInventory CharacterInventory;

        [ReadOnly]
        [ShowInInspector]
        private List<UIInventoryItem> _inventorySlots = new List<UIInventoryItem>();

        [SerializeField]
        private UIInventoryItem _uIInventoryItemPrefab;

        protected override void Start()
        {
            base.Start();

            if (CharacterInventory != null)
            {
                for (int i = 0; i < CharacterInventory.InventorySize; i++)
                {
                    _inventorySlots.Add(Instantiate(_uIInventoryItemPrefab, gameObject.transform));
                }

                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }

            if (_UIManager != null)
            {
                _UIManager.ActionInventory += SetActive;
            }
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            var z = _inventorySlots.Where(x => !x.HasItem);
            _inventorySlots.Where(x => !x.HasItem).First().SetItem(item);
        }
    }
}