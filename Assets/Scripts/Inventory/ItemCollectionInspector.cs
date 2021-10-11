//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEditor;

//namespace Assets.Scripts.Inventory
//{
//    [CustomEditor(typeof(ItemCollection), true)]
//    public class ItemCollectionInspector : Editor
//    {
//        private SerializedProperty Items;

//        private void OnEnable()
//        {
//            Items = serializedObject.FindProperty("_items");
//        }

//        public override void OnInspectorGUI()
//        {
//            var items = (ItemCollection)Items.serializedObject.targetObject;
//            if (items != null && items.Items != null)
//            {
//                foreach (var item in items.Items)
//                {
//                    if (item.ItemData != null)
//                    {
//                        if (item.Stack <= 0)
//                        {
//                            item.Stack = 1;
//                        }
//                        if (item.ItemData.IsStackable && item.Stack > item.ItemData.MaxStack)
//                        {
//                            item.Stack = item.ItemData.MaxStack;
//                        }
//                        if (!item.ItemData.IsStackable && item.Stack > 1)
//                        {
//                            item.Stack = 1;
//                        }
//                    }
//                    else if (item.Stack != 0)
//                    {
//                        item.Stack = 0;
//                    }
//                }
//            }

//            serializedObject.ApplyModifiedPropertiesWithoutUndo();
//            base.OnInspectorGUI();
//        }
//    }
//}
