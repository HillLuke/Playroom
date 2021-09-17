using Assets.Scripts.InventorySystem.Items;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts.UI.InventorySystem
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"UIInventoryItem OnPointerClick {eventData}");

            if (Clicked != null)
            {
                Clicked.Invoke(eventData);
            }
            // propagate event further up
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerClickHandler);

            //if (eventData.button == PointerEventData.InputButton.Right)
            //{
            //    switch (_item.ItemType)
            //    {
            //        case ItemType.Consumable:
            //            break;
            //        case ItemType.Weapon:
            //            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            //            player.CharacterEquipment.EquipAtSlot(Character.Slot.RightHand, ((Weapon)_item).WeaponEquipPrefab);
            //            break;
            //    }
            //}
        }
    }
}