using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIItemSlot : MonoBehaviour
    {
        public Action<PointerEventData> Clicked;

        [ShowInInspector]
        public bool HasItem { get { return _item != null; } }

        [SerializeField]
        private Image _slotIcon;

        private Item _item;

        public void SetItem(Item item)
        {
            _item = item;
            _slotIcon.overrideSprite = item.Icon;
        }
    }
}