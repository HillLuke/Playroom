using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Inventory.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Base Item")]
    public class ItemData : ScriptableObject
    {
        [HorizontalGroup("Split", 55, LabelWidth = 0)]
        [PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite Icon;

        public GameObject WorldItem;

        public string ItemName;
        public string Description;
        public bool IsStackable;

        [ShowIf("IsStackable")]
        public int MaxStack;

        [ShowInInspector]
        public virtual ItemType ItemType { get; set; }

        public virtual bool Use()
        {
            return true;
        }

        public override bool Equals(object other)
        {
            return ItemName.Equals(((ItemData)other).ItemName);
        }
    }

    public enum ItemType
    {
        Consumable,
        Weapon,
    }
}