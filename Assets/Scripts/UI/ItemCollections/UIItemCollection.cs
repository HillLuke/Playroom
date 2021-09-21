using Assets.Scripts.Inventory;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIItemCollection : UIBase
    {
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
                if (_itemCollection.Items.ElementAtOrDefault(i) != null)
                {
                    _slots[i].SetItem(_itemCollection.Items[i]);
                }
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
    }
}