using Assets.Scripts.Inventory.Items;
using System;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class Item
    {
        public ItemData ItemData { get { return _itemData; } }

        public bool HasItem { get { return _itemData != null && _stack > 0; } }

        public int Stack
        {
            //get { ValidateStack(_stack); return _stack; } 
            //set { ValidateStack(value); } 
            get { return _stack; }
            set { _stack = value; }
        }

        [SerializeField]
        private ItemData _itemData;
        [SerializeField]
        private int _stack;

        public Item(ItemData itemData, int stack)
        {
            _itemData = itemData;
            _stack = stack;
        }

        private void ValidateStack(int value)
        {
            _stack = _itemData == null ? 0 : Mathf.Clamp(value, 0, _itemData.MaxStack == 0 ? 0 : _itemData.MaxStack);
        }

    }
}
