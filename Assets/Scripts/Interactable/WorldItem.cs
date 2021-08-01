using Assets.Scripts.Character;
using Assets.Scripts.Inventory.Items;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public class WorldItem : InteractableBase
    {
        public Item ItemData;

        [SerializeField]
        private GameObject _spawnLocation;

        private GameObject _itemModel;

        public override string InteractUIMessage => $"Pickup {ItemData.ItemName} ({_inputManager.PlayerInputData.Use})";

        private void Start()
        {
            if (ItemData != null && _itemModel == null)
            {
                _itemModel = Instantiate(ItemData.WorldItem, _spawnLocation.transform);
            }
        }

        public override void Interact(GameObject Interactor)
        {
            var inventory = Interactor.GetComponent<CharacterInventory>();

            if (inventory != null)
            {
                inventory.AddItem(ItemData);
                Destroy(gameObject);
            }
        }
    }
}