using Assets.Scripts.Inventory.Items;
using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.UI.ItemCollections
{
    public class UIInventory : UIItemCollection
    {
        protected override void Setup()
        {
            _itemCollection = _activePlayer.GetComponent<ItemCollection>();

            if (_itemCollection != null)
            {
                _itemCollection.ActionItemAdded -= CharacterInventory_ActionItemAdded;
                _itemCollection.ActionItemAdded += CharacterInventory_ActionItemAdded;
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            base.Setup();
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            if (_isActive)
            {
                Draw();
            }
        }
    }
}