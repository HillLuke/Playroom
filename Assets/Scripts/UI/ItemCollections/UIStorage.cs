using Assets.Scripts.Interactable;
using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIStorage : UIItemCollection
    {
        private Storage _storage;
        private GameObject _interactor;
        private ItemCollection _interactorItemCollection;

        protected override void Setup()
        {
            _UIManager.ActionOpenStorage -= OpenStorage;
            _UIManager.ActionOpenStorage += OpenStorage;
            _UIManager.ActionCloseStorage -= OnClose;
            _UIManager.ActionCloseStorage += OnClose;

            base.Setup();
        }

        public void OpenStorage(Storage storage, GameObject Interactor)
        {
            _storage = storage;
            _interactor = Interactor;

            _itemCollection = _storage.ItemCollection;

            _itemCollection.ActionItemAdded -= Draw;
            _itemCollection.ActionItemAdded += Draw;
            _itemCollection.ActionItemRemoved -= Draw;
            _itemCollection.ActionItemRemoved += Draw;

            var toLink = _interactor.GetComponent<ItemCollection>();

            if (toLink == null)
            {
                toLink = _interactor.GetComponentInParent<ItemCollection>();
            }
            if (toLink == null)
            {
                toLink = _interactor.GetComponentInChildren<ItemCollection>();
            }

            if (toLink != null)
            {
                _interactorItemCollection = toLink;
            }

            _UIManager.InteractWithUICollection(this);

            OnOpen();
        }

        protected override void OnClose()
        {
            Debug.Log($"CloseStorage _isActive {_isActive}");
            _UIManager.StopInteractWithUICollection(this);
            if (_itemCollection != null)
            {
                _itemCollection.ActionItemAdded -= Draw;
                _itemCollection.ActionItemRemoved -= Draw;
                _itemCollection = null;
            }
            base.OnClose();
        }

        public void TakeAll()
        {
            if (_interactorItemCollection != null)
            {
                for (int i = 0; i < _itemCollection.Items.Count; i++)
                {
                    var item = _itemCollection.Items[i];
                    if (item == null || item.ItemData == null || item.Stack <= 0)
                    {
                        continue;
                    }
                    var used = _interactorItemCollection.AddItem(item);
                    if (used > 0)
                    {
                        item.Stack -= used;
                    }
                    if (item.Stack <= 0)
                    {
                        _itemCollection.RemoveItem(i, item);
                    }
                }
                _itemCollection.ReShuffle();
                Draw();
            }
        }

        public override void RightClick(int index, Item item)
        {
            if (_interactor == null || item == null || item.ItemData == null || item.Stack <= 0)
            {
                return;
            }

            var used = _interactorItemCollection.AddItem(-1, new Item(item.ItemData, item.Stack));
            if (used > 0)
            {
                item.Stack -= used;
            }

            if (item.Stack <= 0)
            {
                _itemCollection.RemoveItem(index, new Item(item.ItemData, item.Stack));
            }

            base.RightClick(index, item);
        }
    }
}