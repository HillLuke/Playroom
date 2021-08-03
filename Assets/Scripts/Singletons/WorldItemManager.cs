using Assets.Scripts.Interactable;
using Assets.Scripts.Inventory.Items;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    public class WorldItemManager : Singleton<WorldItemManager>
    {
        public WorldItem WorldItemPrefab;
        public float DropForce;

        private UnityEngine.Camera _camera;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;
        }

        public void SpawnItem(Item item, GameObject parent)
        {
            var temp = Instantiate(WorldItemPrefab, parent.transform.position, parent.transform.rotation);
            temp.ItemData = item;
            temp.GetComponent<Rigidbody>().AddForce(parent.transform.forward * DropForce);

            // drop in direction of camera
            //temp.GetComponent<Rigidbody>().AddForce(parent.transform.forward * DropForce);
        }
    }
}