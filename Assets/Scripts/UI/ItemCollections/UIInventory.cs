using Assets.Scripts.Inventory.Items;
using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

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
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            base.Setup();
        }
    }
}