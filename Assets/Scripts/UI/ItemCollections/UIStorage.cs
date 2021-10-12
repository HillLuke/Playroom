using Assets.Scripts.Interactable;
using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIStorage : UIItemCollection
    {
        private Storage _storage;
        private GameObject _interactor;

        protected override void Setup()
        {
            _uIManager.ActionOpenStorage -= OpenStorage;
            _uIManager.ActionOpenStorage += OpenStorage;
            _uIManager.ActionCloseStorage -= OnClose;
            _uIManager.ActionCloseStorage += OnClose;

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
                _interactingWith = toLink;
            }

            _uIManager.InteractWithUICollection(this);

            OnOpen();
        }

        protected override void OnClose()
        {
            if (_isActive)
            {
                _uIManager.StopInteractWithUICollection(this);
                if (_itemCollection != null)
                {
                    _itemCollection.ActionItemAdded -= Draw;
                    _itemCollection.ActionItemRemoved -= Draw;
                    _itemCollection = null;
                }
            }
            base.OnClose();
        }

        public void TakeAll()
        {
            if (_interactingWith != null)
            {
                for (int i = 0; i < _itemCollection.Items.Count; i++)
                {
                    var item = _itemCollection.Items[i];
                    if (item == null || item.ItemData == null || item.Stack <= 0)
                    {
                        continue;
                    }
                    var used = _interactingWith.AddItem(item);
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
    }
}