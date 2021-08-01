using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterEquipment : MonoBehaviour
    {
        public EquipmentSlot SlotHead;
        public EquipmentSlot SlotChest;
        public EquipmentSlot SlotLegs;
        public EquipmentSlot SlotFeet;
        public EquipmentSlot SlotLeftHand;
        public EquipmentSlot SlotRightHand;

        private Dictionary<Slot, EquipmentSlot> SlotsMap = new Dictionary<Slot, EquipmentSlot>();

        private void Start()
        {
            SlotsMap.Add(Slot.Head, SlotHead);
            SlotsMap.Add(Slot.Chest, SlotChest);
            SlotsMap.Add(Slot.Legs, SlotLegs);
            SlotsMap.Add(Slot.Feet, SlotFeet);
            SlotsMap.Add(Slot.LeftHand, SlotLeftHand);
            SlotsMap.Add(Slot.RightHand, SlotRightHand);
        }

        public void EquipAtSlot(Slot slot, GameObject gameObject)
        {
            var equipmentSlot = SlotsMap[slot];
            if (equipmentSlot.SpawnedEquipment != null)
            {
                Destroy(equipmentSlot.SpawnedEquipment);
            }

            equipmentSlot.SpawnedEquipment = Instantiate(gameObject, equipmentSlot.Slot.transform.position, equipmentSlot.Slot.transform.rotation, equipmentSlot.Slot.transform);
        }
    }

    [Serializable]
    public class EquipmentSlot
    {
        public GameObject Slot;
        public Slot SlotType;
        public GameObject SpawnedEquipment;
    }

    public enum Slot
    {
        Head,
        Chest,
        Legs,
        Feet,
        LeftHand,
        RightHand
    }
}