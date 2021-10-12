using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIInventory : UIItemCollection
    {
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

            _uIManager.ActionInteractWithUICollection += InteractWithCollection;
            _uIManager.ActionStopInteractWithUICollection += InteractWithCollection;

            base.Setup();
        }

        private void InteractWithCollection(UIItemCollection collection)
        {
            _interactingWith = collection.ItemCollection;
        }
    }
}