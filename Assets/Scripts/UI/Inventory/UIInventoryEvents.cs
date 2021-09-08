using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class UIInventoryEvents : UIBase
    {
        public CharacterInventory CharacterInventory;
        public UIInventoryEventText UIInventoryEventText;

        protected override void Setup()
        {
            CharacterInventory = _activePlayer?.CharacterInventory;

            if (CharacterInventory != null)
            {
                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
                CharacterInventory.ActionItemDropped += CharacterInventory_ActionItemDropped;
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