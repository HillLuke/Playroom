using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Weapon")]
    public class Weapon : Item
    {
        public override ItemType ItemType { get => ItemType.Weapon; set => ItemType = value; }

        public int Damage;
        public GameObject WeaponEquipPrefab;

        public override bool Use()
        {
            return base.Use();
        }
    }
}
