using Assets.Scripts.Inventory;
using Assets.Scripts.Singletons;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
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
        private InputManager _inputManager;
        private UIManager _uIManager;
        private UIItemCollection _parentContainer;
        private int _index;
        private Coroutine _delayTooltipCoroutine;

        private void Start()
        {
            _dragManager = DragManager.instance;
            _parentContainer = gameObject.GetComponentInParent<UIItemCollection>();

            if (UIManager.instanceExists)
            {
                _uIManager = UIManager.instance;
            }
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;
            }
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

        private void SetTooltip(Item item)
        {
            if (_delayTooltipCoroutine != null)
            {
                StopCoroutine(_delayTooltipCoroutine);
            }

            _delayTooltipCoroutine = StartCoroutine(Tooltip(item != null ? 0.2f : 0f, item));
        }

        #region Interfaces

        public void OnDrop(PointerEventData eventData)
        {
            if (_parentContainer.ItemCollection.CanDragIn && _dragManager.DragObject != null)
            {
                _parentContainer.StackOrSwap(this, _dragManager.DragObject.UIItemSlot);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Item != null && _parentContainer.ItemCollection.CanDragOut)
            {
                _dragManager.SetDragObject(this);
                SetTooltip(null);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData?.pointerEnter?.layer != (int)Layers.UI ||
                eventData?.pointerEnter?.gameObject.GetComponentInParent<UIItemCollection>() == null &&
                _parentContainer.ItemCollection.CanDropItems)
            {
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
            if (eventData.button == PointerEventData.InputButton.Left && _inputManager.UIShift)
            {
                _parentContainer.ShiftLeftClick(_index, Item);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_dragManager.DragObject == null)
            {
                SetTooltip(_item);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetTooltip(null);
        }

        private IEnumerator Tooltip(float delay, Item item)
        {
            float time = 0.0f;
            yield return true;
            while (time < delay)
            {
                time += Time.deltaTime;
                yield return true;
            }
            _uIManager?.HoverOverItem(item);
        }

        #endregion
    }
}