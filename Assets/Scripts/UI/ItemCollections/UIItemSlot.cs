using Assets.Scripts.Inventory;
using Assets.Scripts.Singletons;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Action<PointerEventData> Clicked;

        public UIItemCollection UIItemCollection { get { return _parentContainer; } }
        public Item Item { get { return _item; } }
        public Image SlotIcon { get { return _slotIcon; } }
        public int Index { get { return _index; } }

        [ShowInInspector]
        public bool HasItem { get { return _item != null; } }

        [SerializeField]
        private Image _slotIcon;
        [SerializeField]
        private TextMeshProUGUI _stack;

        private Item _item;
        private DragManager _dragManager;
        private UIItemCollection _parentContainer;
        private int _index;

        private void Start()
        {
            _dragManager = DragManager.instance;
            _parentContainer = gameObject.GetComponentInParent<UIItemCollection>();
        }

        public void SetItem(Item item, int index)
        {
            _item = item;
            _slotIcon.overrideSprite = item?.ItemData?.Icon ?? null;
            var alpha = (_slotIcon.overrideSprite == null) ? 0f : 255f;
            _slotIcon.color = new Color(_slotIcon.color.r, _slotIcon.color.g, _slotIcon.color.b, alpha);
            _index = index;
            if (item != null && item.ItemData != null && item.ItemData.IsStackable && item.Stack != 0)
            {
                _stack.text = item.Stack.ToString();
                _stack.gameObject.SetActive(true);
            }
            else
            {
                _stack.gameObject.SetActive(false);
            }
        }

        #region Interfaces

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("UIItemSlot OnDrop");
            if (_parentContainer.ItemCollection.CanDragIn && _dragManager.DragObject != null)
            {
                _parentContainer.StackOrSwap(this, _dragManager.DragObject.UIItemSlot);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("UIItemSlot OnBeginDrag");
            if (Item != null && _parentContainer.ItemCollection.CanDragOut)
            {
                _dragManager.SetDragObject(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("UIItemSlot OnEndDrag");

            if (eventData?.pointerEnter?.layer != (int)Layers.UI ||
                eventData?.pointerEnter?.gameObject.GetComponentInParent<UIItemCollection>() == null &&
                _parentContainer.ItemCollection.CanDropItems)
            {
                Debug.Log("Drop item");
                var parentTransform = _parentContainer.ItemCollection.gameObject.GetComponentInParent<Transform>();

                if (parentTransform == null)
                {
                    parentTransform = _parentContainer.ItemCollection.gameObject.GetComponent<Transform>();
                }

                WorldItemManager.instance.SpawnItem(Item, parentTransform.gameObject);

                _parentContainer.ItemCollection.RemoveItem(_index, Item);
            }

            _dragManager.ClearDragObject();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _parentContainer.RightClick(_index, Item);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        #endregion
    }
}