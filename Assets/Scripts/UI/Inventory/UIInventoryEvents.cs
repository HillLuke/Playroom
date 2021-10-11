using Assets.Scripts.Inventory;
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
                Inventory.ActionItemAdded -= ItemAdded;
                Inventory.ActionItemDropped -= ItemDropped;
                Inventory.ActionItemAdded += ItemAdded;
                Inventory.ActionItemDropped += ItemDropped;
            }
            else
            {
                Debug.LogError("CharacterInventory is null");
            }

            base.Setup();
        }

        private void ItemDropped(Item item)
        {
            var notification = Instantiate(UIInventoryEventText, gameObject.transform);

            if (item.Stack > 1)
            {
                notification.SetText($"Dropped {item.ItemData.ItemName} x {item.Stack}");
            }
            else
            {
                notification.SetText($"Dropped {item.ItemData.ItemName}");
            }
        }

        private void ItemAdded(Item item)
        {
            var notification = Instantiate(UIInventoryEventText, gameObject.transform);

            if (item.Stack > 1)
            {
                notification.SetText($"Picked up {item.ItemData.ItemName} x {item.Stack}");
            }
            else
            {
                notification.SetText($"Picked up {item.ItemData.ItemName}");
            }
        }
    }
}