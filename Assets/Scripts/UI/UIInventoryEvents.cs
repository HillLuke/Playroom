using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using UnityEngine;

public class UIInventoryEvents : MonoBehaviour
{
    public CharacterInventory CharacterInventory;
    public UIInventoryEventText UIInventoryEventText;

    private void Start()
    {
        if (CharacterInventory != null)
        {
            CharacterInventory.ActionItemAdded += CharacterInventory_ActionItemAdded;
        }
    }

    private void CharacterInventory_ActionItemAdded(Item item)
    {
        var notification = Instantiate(UIInventoryEventText, gameObject.transform);
        notification.SetText($"Picked up {item.ItemName}");
    }
}
