using Assets.Scripts.Inventory;
using Sirenix.OdinInspector;
using System.Collections.Generic;
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

        [SerializeField]
        private protected GameObject _slotHolder;

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
                _slots.Add(Instantiate(_uIInventoryItemPrefab, _slotHolder.transform));
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

        public bool StackOrSwap(UIItemSlot target, UIItemSlot source)
        {
            var tempTarget = new Item(target?.Item?.ItemData, target?.Item?.Stack ?? 0);
            var tempSource = new Item(source?.Item?.ItemData, source?.Item?.Stack ?? 0);
            var isSameCollection = target.UIItemCollection == source.UIItemCollection;
            var result = false;

            if (isSameCollection)
            {
                result = target.UIItemCollection.ItemCollection.StackOrSwap(target.Index, source.Index);
            }
            else
            {
                var used = target.UIItemCollection.ItemCollection.AddItem(target.Index, new Item(source.Item.ItemData, source.Item.Stack));
                if (used > 0)
                {
                    source.Item.Stack -= used;
                }

                if (source.Item.Stack <= 0)
                {
                    source.UIItemCollection.ItemCollection.RemoveItem(source.Index, new Item(source.Item.ItemData, source.Item.Stack));
                }
            }

            if (target.UIItemCollection != source.UIItemCollection)
            {
                //Refresh both collections
                target.UIItemCollection.Draw();
                source.UIItemCollection.Draw();
            }
            else
            {
                Draw();
            }

            return result;
        }

        public virtual void RightClick(int index, Item item) { }

        #region Interfaces

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("UIItemCollection OnDrop");
        }

        #endregion Interfaces
    }
}