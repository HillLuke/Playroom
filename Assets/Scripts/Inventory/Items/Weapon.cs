using UnityEngine;

namespace Assets.Scripts.Inventory.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Weapon")]
    public class Weapon : ItemData
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