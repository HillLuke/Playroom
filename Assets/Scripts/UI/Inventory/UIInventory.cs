using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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

            if (_UIManager != null)
            {
                _UIManager.ActionInventory += SetActive;
            }
        }

        protected override void Setup()
        {
            if (CharacterInventory != null)
            {
                for (int i = 0; i < CharacterInventory.InventorySize; i++)
                {
                    _inventorySlots.Add(Instantiate(_uIInventoryItemPrefab, gameObject.transform));
                }

                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }

            base.Setup();
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            _inventorySlots.Where(x => !x.HasItem).First().SetItem(item);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("click");
        }

        public void ItemClicked(PointerEventData eventData)
        {

        }
    }
}