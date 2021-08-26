using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventoryItem : MonoBehaviour
    {
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