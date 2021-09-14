using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventory : UIBase, IPointerClickHandler
    {
        private CharacterInventory _characterInventory;

        [ReadOnly]
        [ShowInInspector]
        private List<UIInventoryItem> _inventorySlots = new List<UIInventoryItem>();

        [SerializeField]
        private UIInventoryItem _uIInventoryItemPrefab;

        public void ItemClicked(PointerEventData eventData)
        {
            Debug.Log($"UIInventory ItemClicked {eventData}");
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"UIInventory OnPointerClick {eventData}");
        }

        protected override void OnClose()
        {
            ClearSlots();
            base.OnClose();
        }

        protected override void OnOpen()
        {
            ClearSlots();

            for (int i = 0; i < _characterInventory.InventorySize; i++)
            {
                _inventorySlots.Add(Instantiate(_uIInventoryItemPrefab, gameObject.transform));
            }

            DrawInventory();

            base.OnOpen();
        }

        protected override void Setup()
        {
            _characterInventory = _activePlayer.GetComponent<CharacterInventory>();

            if (_characterInventory != null)
            {
                _characterInventory.ActionItemAdded -= CharacterInventory_ActionItemAdded;
                _characterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            base.Setup();
        }

        private void DrawInventory()
        {
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                if (_characterInventory.Inventory.ElementAtOrDefault(i) != null)
                {
                    _inventorySlots[i].SetItem(_characterInventory.Inventory[i]);
                }
            }
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            if (_isActive)
            {
                DrawInventory();
            }
        }

        private void ClearSlots()
        {
            foreach (var item in _inventorySlots)
            {
                item.gameObject.SetActive(false);
                Destroy(item.gameObject);
            }
            _inventorySlots = new List<UIInventoryItem>();
        }
    }
}