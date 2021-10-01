﻿using Assets.Scripts.Interactable;
using Assets.Scripts.Inventory;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    public class WorldItemManager : Singleton<WorldItemManager>
    {
        public WorldItem WorldItemPrefab;
        public float DropForceForward;
        public float DropForceUp;

        public void SpawnItem(Item item, GameObject parent)
        {
            var temp = Instantiate(WorldItemPrefab, parent.transform.position + Vector3.up * 0.3f, parent.transform.rotation);
            temp.Item = item;
            temp.GetComponent<Rigidbody>().AddForce(parent.transform.forward * DropForceForward);
            temp.GetComponent<Rigidbody>().AddForce(parent.transform.up * DropForceUp);
        }
    }
}