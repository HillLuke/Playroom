using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Character.CharacterProperties.Modifier;
using static Assets.Scripts.Character.CharacterProperties.Property;

namespace Assets.Scripts.Character
{
    [CreateAssetMenu(fileName = "CharacterProperties", menuName = "CharacterProperties/New CharacterProperties")]
    public class CharacterProperties : SerializedScriptableObject
    {
        /// <summary>
        /// List of properties available to a character.
        /// Would rather this be a dictionary but unity doesnt serialize them well and dont want to make my own serialisable dictionary.
        /// </summary>
        public List<Property> Properties = new List<Property>();

        public CharacterProperties()
        {
            // Add each property, saves time creating new character properties
            foreach (var pt in Enum.GetValues(typeof(PropertyType)))
            {
                Properties.Add(new Property() { Type = (PropertyType)pt });
            }
        }

        public Property GetProperty(PropertyType propertyType)
        {
            return Properties.Find(x => x.Type == propertyType);
        }

        public float GetPropertyFinalValue(PropertyType propertyType)
        {
            return Properties.Find(x => x.Type == propertyType).FinalValue;
        }

        public void ApplyModifierToProperty(PropertyType propertyType, Modifier modifier)
        {
            GetProperty(propertyType).Modifiers.Add(modifier);
        }

        public void RemoveModifierFromProperty(PropertyType propertyType, Modifier modifier)
        {
            GetProperty(propertyType).Modifiers.Remove(modifier);
        }

        [Serializable]
        public class Property
        {
            public PropertyType Type;
            public float BaseValue;
            public List<Modifier> Modifiers = new List<Modifier>();
            private List<ModifierType> ModifierApplyOrder = new List<ModifierType>() { ModifierType.FlatBase, ModifierType.PercentBase, ModifierType.FlatFinal, ModifierType.PercentFinal };
            public float FinalValue => GetFinalValue();

            public float GetFinalValue()
            {
                var finalValue = BaseValue;
                var percentBaseValue = 0f;
                var percentFinalValue = 0f;

                foreach (var modifierOrderType in ModifierApplyOrder)
                {
                    var selectedModifiersOfType = Modifiers.Where(x => x.Type == modifierOrderType);

                    foreach (var modifier in selectedModifiersOfType)
                    {
                        switch (modifier.Type)
                        {
                            case ModifierType.FlatBase:
                            case ModifierType.FlatFinal:
                                finalValue += modifier.Value;
                                break;

                            case ModifierType.PercentBase:
                                percentBaseValue += modifier.Value;
                                if (modifier == selectedModifiersOfType.Last())
                                {
                                    finalValue = finalValue += (BaseValue * percentBaseValue);
                                }
                                break;

                            case ModifierType.PercentFinal:
                                percentFinalValue += modifier.Value;
                                if (modifier == selectedModifiersOfType.Last())
                                {
                                    finalValue = finalValue += (finalValue * percentFinalValue);
                                }
                                break;

                            default:
                                Debug.LogError("ModifierType not found");
                                break;
                        }
                    }
                }

                return (float)Math.Round(finalValue, 4);
            }

            public enum PropertyType
            {
                MaxJumps,
                JumpForce,
                MovementSharpness,
                Speed,
                RunningSpeedMultiplier,
                WalkingSpeedMultiplier,
                Gravity
            }
        }

        [Serializable]
        public class Modifier
        {
            public enum ModifierType
            {
                /// <summary>
                /// Change the base value by a flat amount +/-
                /// </summary>
                FlatBase,

                /// <summary>
                /// Change the final value by a flat amount +/-
                /// </summary>
                FlatFinal,

                /// <summary>
                /// Change the base value by a percentage
                /// </summary>
                PercentBase,

                /// <summary>
                /// Change the final value after Flat and PercentBase has been calculated
                /// </summary>
                PercentFinal
            }

            public ModifierType Type;
            public float Value;
        }
    }
}