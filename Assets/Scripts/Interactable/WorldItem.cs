using Assets.Scripts.Character;
using Assets.Scripts.InventorySystem;
using Assets.Scripts.InventorySystem.Items;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public class WorldItem : InteractableBase
    {
        public Item ItemData;

        [SerializeField]
        private GameObject _spawnLocation;

        private GameObject _itemModel;

        public override string InteractUIMessage => $"Pickup  {ItemData.ItemName} ({_inputManager.PlayerInputData.Action_Use.KeyCode})";

        private void Awake()
        {
            if (ItemData != null && _itemModel == null)
            {
                _itemModel = Instantiate(ItemData.WorldItem, gameObject.transform);
                _itemModel.layer = gameObject.layer;
                _itemModel.AddComponent<BoxCollider>().isTrigger = true;
                _itemModel.AddComponent<BoxCollider>();
            }
        }

        public override void Interact(GameObject Interactor)
        {
            var inventory = Interactor.GetComponent<Inventory>();

            if (inventory != null && inventory.AddItem(ItemData))
            {
                Destroy(gameObject);
            }
        }


        private void OnDrawGizmos()
        {
            var mesh = ItemData.WorldItem.GetComponentInChildren<MeshFilter>();
            if (mesh != null && mesh.sharedMesh != null)
            {
                Gizmos.color = GizmoColours.WORLD_ITEM;
                Gizmos.DrawMesh(mesh.sharedMesh, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}