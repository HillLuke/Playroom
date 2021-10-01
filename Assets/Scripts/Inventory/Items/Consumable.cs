using UnityEngine;

namespace Assets.Scripts.Inventory.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Consumable")]
    public class Consumable : ItemData
    {
        [SerializeReference]
        public ConsumableBehaviour ConsumableBehaviour; // Can be expanded in to a list to provide multiple behaviours to a single consumable

        public override ItemType ItemType { get => ItemType.Consumable; set => ItemType = value; }

        public override bool Use()
        {
            if (ConsumableBehaviour != null)
            {
                ConsumableBehaviour.Consume();
            }

            return base.Use();
        }
    }

    /// <summary>
    /// Base behaviour for different consumable types
    /// e.g. Food, potions, usables
    /// </summary>
    public class ConsumableBehaviour
    {
        public virtual void Consume()
        {
        }
    }

    public class ComsumableFood : ConsumableBehaviour
    {
        public int FoodAmount;

        public override void Consume()
        {
            base.Consume();
        }
    }

    public class HealingPotion : ConsumableBehaviour
    {
        public int HealAmount;

        public override void Consume()
        {
            base.Consume();
        }
    }
}