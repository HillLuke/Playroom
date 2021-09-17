using UnityEngine;

namespace Assets.Scripts.InventorySystem.Items
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