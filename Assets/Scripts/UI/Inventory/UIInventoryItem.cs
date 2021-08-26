using Assets.Scripts.Inventory.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"Clicked {_item.ItemName}");
            
        }

    }
}