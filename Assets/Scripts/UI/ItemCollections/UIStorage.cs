using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIStorage : UIItemCollection
    {
        protected override void Setup()
        {
            _UIManager.ActionOpenStorage -= OpenStorage;
            _UIManager.ActionOpenStorage += OpenStorage;
            _UIManager.ActionCloseStorage -= CloseStorage;
            _UIManager.ActionCloseStorage += CloseStorage;

            base.Setup();
        }

        public void OpenStorage(ItemCollection itemCollection)
        {
            _itemCollection = itemCollection;

            _itemCollection.ActionItemAdded -= Draw;
            _itemCollection.ActionItemAdded += Draw;
            _itemCollection.ActionItemRemoved -= Draw;
            _itemCollection.ActionItemRemoved += Draw;

            ToggleActive();
        }

        public void CloseStorage()
        {
            Debug.Log($"CloseStorage _isActive {_isActive}");
            if (_isActive)
            {
                _itemCollection = null;
                ToggleActive();
            }
        }
    }
}