using Assets.Scripts.Interactable;
using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIInventory : UIItemCollection
    {
        private UIItemCollection _interactingWith;

        protected override void Setup()
        {
            _itemCollection = _activePlayer.GetComponent<ItemCollection>();

            if (ItemCollection != null)
            {
                ItemCollection.ActionItemAdded -= Draw;
                ItemCollection.ActionItemAdded += Draw;
                ItemCollection.ActionItemRemoved -= Draw;
                ItemCollection.ActionItemRemoved += Draw;
                ItemCollection.ActionItemDropped -= Draw;
                ItemCollection.ActionItemDropped += Draw;
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            _UIManager.ActionInteractWithUICollection += (x) =>
            {
                    _interactingWith = x;
            };

            _UIManager.ActionStopInteractWithUICollection += (x) =>
            {
                _interactingWith = null;
            };

            base.Setup();
        }


        public override void RightClick(int index, Item item)
        {
            if (_interactingWith == null || item == null || item.ItemData == null || item.Stack <= 0)
            {
                return;
            }

            var used = _interactingWith.ItemCollection.AddItem(-1, new Item(item.ItemData, item.Stack));
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