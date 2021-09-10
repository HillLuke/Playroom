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

        public void ItemClicked(PointerEventData eventData)
        {
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("click");
        }

        protected override void OnClose()
        {
            ClearSlots();
            base.OnClose();
        }

        protected override void OnOpen()
        {
            ClearSlots();

            for (int i = 0; i < CharacterInventory.InventorySize; i++)
            {
                _inventorySlots.Add(Instantiate(_uIInventoryItemPrefab, gameObject.transform));
            }

            base.OnOpen();
        }

        protected override void Setup()
        {
            CharacterInventory = _activePlayer.GetComponent<CharacterInventory>();

            if (CharacterInventory != null)
            {
                CharacterInventory.ActionItemAdded -= CharacterInventory_ActionItemAdded;
                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            base.Setup();
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            _inventorySlots.Where(x => !x.HasItem).First().SetItem(item);
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