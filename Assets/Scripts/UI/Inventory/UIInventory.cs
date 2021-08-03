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
        private List<UIInventoryItem> _inventorySlots = new List<UIInventoryItem>();

        [SerializeField]
        private UIInventoryItem _uIInventoryItemPrefab;

        private void Start()
        {
            if (CharacterInventory != null)
            {
                for (int i = 0; i < CharacterInventory.InventorySize; i++)
                {
                    _inventorySlots.Add(Instantiate(_uIInventoryItemPrefab, gameObject.transform));
                }

                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            var z = _inventorySlots.Where(x => !x.HasItem);
            _inventorySlots.Where(x => !x.HasItem).First().SetItem(item);
        }
    }
}