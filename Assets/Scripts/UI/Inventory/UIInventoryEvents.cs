using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.Items;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventoryEvents : UIBase
    {
        public ItemCollection Inventory;
        public UIInventoryEventText UIInventoryEventText;

        protected override void Setup()
        {
            Inventory = _activePlayer?.Inventory;

            if (Inventory != null)
            {
                Inventory.ActionItemAdded -= CharacterInventory_ActionItemAdded;
                Inventory.ActionItemDropped -= CharacterInventory_ActionItemDropped;
                Inventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
                Inventory.ActionItemDropped += CharacterInventory_ActionItemDropped;
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            base.Setup();
        }

        private void CharacterInventory_ActionItemDropped(Item item)
        {
            var notification = Instantiate(UIInventoryEventText, gameObject.transform);
            notification.SetText($"Dropped {item.ItemName}");
        }

        private void CharacterInventory_ActionItemAdded(Item item)
        {
            var notification = Instantiate(UIInventoryEventText, gameObject.transform);
            notification.SetText($"Picked up {item.ItemName}");
        }
    }
}