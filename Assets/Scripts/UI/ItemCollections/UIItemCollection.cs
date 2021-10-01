using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIItemCollection : UIBase, IDropHandler
    {
        public ItemCollection ItemCollection { get { return _itemCollection; } }

        private protected ItemCollection _itemCollection;

        [ReadOnly]
        [ShowInInspector]
        private protected List<UIItemSlot> _slots = new List<UIItemSlot>();

        [SerializeField]
        private protected UIItemSlot _uIInventoryItemPrefab;

        protected override void OnClose()
        {
            ClearSlots();
            base.OnClose();
        }

        protected override void OnOpen()
        {
            ClearSlots();

            for (int i = 0; i < _itemCollection.InventorySize; i++)
            {
                _slots.Add(Instantiate(_uIInventoryItemPrefab, gameObject.transform));
            }

            Draw();

            base.OnOpen();
        }

        private protected void Draw()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetItem(_itemCollection.Items[i], i);
            }
        }

        private protected void ClearSlots()
        {
            foreach (var item in _slots)
            {
                item.gameObject.SetActive(false);
                Destroy(item.gameObject);
            }
            _slots = new List<UIItemSlot>();
        }

        private protected void Draw(Item item)
        {
            if (_isActive)
            {
                Draw();
            }
        }

        public void StackOrSwap(UIItemSlot slot1, UIItemSlot slot2)
        {
            if (slot1.Item == slot2.Item)
            {
                slot1.Item.Stack++;
            }
            else
            {
                (
                    slot1.UIItemCollection.ItemCollection.Items[slot1.Index],
                    slot2.UIItemCollection.ItemCollection.Items[slot2.Index]
                ) =
                (
                    slot2.UIItemCollection.ItemCollection.Items[slot2.Index],
                    slot1.UIItemCollection.ItemCollection.Items[slot1.Index]
                );
            }

            if (slot1.UIItemCollection != slot2.UIItemCollection)
            {
                //Refresh both collections
                slot1.UIItemCollection.Draw();
                slot2.UIItemCollection.Draw();
            }
            else
            {
                Draw();
            }
        }

        #region Interfaces

        public void OnDrop(PointerEventData eventData)
        {
        }

        #endregion
    }
}