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

            if (_itemCollection == null)
            {
                Debug.LogError("ItemCollection is null");
            }

            base.Setup();
        }

        public void OpenStorage(ItemCollection itemCollection)
        {
            _itemCollection = itemCollection;
            ToggleActive();
        }
    }
}