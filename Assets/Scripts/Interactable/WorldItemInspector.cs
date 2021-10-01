using Assets.Scripts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.Interactable
{
    [CustomEditor(typeof(WorldItem), true)]
    public class WorldItemInspector : Editor
    {
        private WorldItem _item;

        private void OnEnable()
        {
            _item = (WorldItem)serializedObject.targetObject;
        }

        public override void OnInspectorGUI()
        {
            if (_item != null && _item.Item != null)
            {
                if (_item.Item.ItemData != null)
                {
                    if (_item.Item.Stack <= 0)
                    {
                        _item.Item.Stack = 1;
                    }
                    if (_item.Item.ItemData.IsStackable && _item.Item.Stack > _item.Item.ItemData.MaxStack)
                    {
                        _item.Item.Stack = _item.Item.ItemData.MaxStack;
                    }
                    if (!_item.Item.ItemData.IsStackable && _item.Item.Stack > 1)
                    {
                        _item.Item.Stack = 1;
                    }
                }
                else if (_item.Item.Stack != 0)
                {
                    _item.Item.Stack = 0;
                }
            }
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            
            base.OnInspectorGUI();
        }
    }
}
