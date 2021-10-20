using Assets.Scripts.Inventory;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public class WorldItem : InteractableBase
    {
        [SerializeField]
        public Item Item;
        public override string InteractUIMessage => InteractMessage();


        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(GameObject Interactor)
        {
            var inventory = Interactor.GetComponent<ItemCollection>();

            if (inventory != null)
            {
                var used = inventory.AddItem(Item);
                if (used > 0)
                {
                    Item.Stack -= used;
                }
            }
            if (Item.Stack <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            var mesh = Item?.ItemData?.WorldItem?.GetComponentInChildren<MeshFilter>();
            if (mesh != null && mesh.sharedMesh != null)
            {
                Gizmos.color = GizmoColours.WORLD_ITEM;
                Gizmos.DrawMesh(mesh.sharedMesh, gameObject.transform.position, gameObject.transform.rotation);
            }
        }

        private string InteractMessage()
        {
            if (Item.Stack > 1)
            {
                return $"Pickup {Item.ItemData.ItemName} x {Item.Stack} ({_inputManager.PlayerInputData.Action_Use.KeyCode})";
            }

            return $"Pickup {Item.ItemData.ItemName} ({_inputManager.PlayerInputData.Action_Use.KeyCode})";
        }
    }
}