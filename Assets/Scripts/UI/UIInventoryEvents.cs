using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIInventoryEvents : MonoBehaviour
    {
        public CharacterInventory CharacterInventory;
        public UIInventoryEventText UIInventoryEventText;

        private void Start()
        {
            if (CharacterInventory != null)
            {
                CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
                CharacterInventory.ActionItemDropped += CharacterInventory_ActionItemDropped;
            }
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