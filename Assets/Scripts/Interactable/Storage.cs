using Assets.Scripts.Inventory;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public class Storage : InteractableBase
    {
        public override string InteractUIMessage => $"Open ({_inputManager.PlayerInputData.Action_Use.KeyCode})";

        private ItemCollection _itemCollection;

        protected override void Awake()
        {
            _itemCollection = gameObject.GetComponent<ItemCollection>();

            if (_itemCollection == null)
            {
                Debug.LogError("ItemCollection is null");
            }

            base.Awake();
        }

        public override void Interact(GameObject Interactor)
        {
            _uIManager.OpenStorage(_itemCollection);
        }
    }
}