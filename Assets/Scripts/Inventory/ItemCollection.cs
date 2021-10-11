using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemCollection : MonoBehaviour
    {
        public Action<Item> ActionItemAdded;
        public Action<Item> ActionItemDropped;
        public Action<Item> ActionItemRemoved;

        public IList<Item> Items => Array.AsReadOnly(_items);

        public int InventorySize = 20;
        public bool CanDragIn;
        public bool CanDragOut;
        public bool CanDropItems;

        [SerializeField]
        private Item[] _items;

        private void Awake()
        {
            if (_items != null)
            {
                Array.Resize(ref _items, InventorySize);
            }
        }

        public int AddItem(Item item)
        {
            return AddItem(-1, item);
        }

        public int AddItem(int index, Item item)
        {
            if (item == null || item.ItemData == null || item.Stack <= 0)
            {
                return 0;
            }

            var amountAdded = 0;
            var nextEmptySlot = Array.FindIndex(_items, x => x == null || !x.HasItem);

            if (index == -1)
            {
                if (item.ItemData.IsStackable)
                {
                    //add to item stacks that have space
                    for (int i = 0; i < _items.Length; i++)
                    {
                        if (_items[i] != null && _items[i].ItemData != null
                            && _items[i].ItemData.Equals(item.ItemData)
                            && _items[i].Stack < _items[i].ItemData.MaxStack)
                        {
                            var diff = _items[i].ItemData.MaxStack - _items[i].Stack;

                            if (diff <= item.Stack)
                            {
                                _items[i].Stack += diff;
                                amountAdded += diff;
                            }
                            else
                            {
                                _items[i].Stack += item.Stack;
                                amountAdded += item.Stack;
                            }
                        }
                    }
                    //add new stacks for remainder
                    if (item.Stack - amountAdded > 0)
                    {
                        nextEmptySlot = Array.FindIndex(_items, x => x == null || !x.HasItem);
                        if (nextEmptySlot != -1)
                        {
                            var diff = item.Stack - amountAdded;
                            _items[nextEmptySlot] = new Item(item.ItemData, diff);
                            amountAdded += diff;
                        }
                    }

                    if (ActionItemAdded != null && amountAdded > 0)
                    {
                        ActionItemAdded.Invoke(new Item(item.ItemData, amountAdded));
                    }
                    return amountAdded;
                }
                else
                {
                    nextEmptySlot = Array.FindIndex(_items, x => x == null || !x.HasItem);
                    if (nextEmptySlot != -1)
                    {
                        amountAdded += item.Stack;
                        _items[nextEmptySlot] = new Item(item.ItemData, amountAdded);
                        if (ActionItemAdded != null && amountAdded > 0)
                        {
                            ActionItemAdded.Invoke(new Item(item.ItemData, amountAdded));
                        }
                        return amountAdded;
                    }
                }
            }
            else
            {
                if (_items[index] == null)
                {
                    _items[index] = new Item(item.ItemData, item.Stack);
                    amountAdded += item.Stack;
                }
                else if (_items[index] != null && _items[index].ItemData != null
                        && _items[index].ItemData.Equals(item.ItemData)
                        && _items[index].Stack <= _items[index].ItemData.MaxStack)
                {
                    var diff = _items[index].ItemData.MaxStack - _items[index].Stack;

                    if (diff > 0 || diff <= item.Stack)
                    {
                        _items[index].Stack += diff;
                        amountAdded += diff;
                    }
                }

                if (ActionItemAdded != null && amountAdded > 0)
                {
                    ActionItemAdded.Invoke(new Item(item.ItemData, amountAdded));
                }
                return amountAdded;
            }

            return 0;
        }

        public void RemoveItem(int index, Item item)
        {
            _items[index] = null;

            if (ActionItemRemoved != null)
            {
                ActionItemRemoved.Invoke(item);
            }
        }

        public bool StackOrSwap(int target, int source)
        {
            if (target == -1 || source == -1 ||
                target > _items.Length - 1 ||
                source > _items.Length - 1 ||
                target == source)
            {
                return false;
            }

            return CanStack(target, source) ? Stack(target, source) : Swap(target, source);
        }

        public bool Swap(int target, int source)
        {
            (
                _items[target],
                _items[source]
            ) =
            (
                _items[source],
                _items[target]
            );

            return true;
        }

        public bool Stack(int target, int source)
        {
            var targetItem = _items[target];
            var sourceItem = _items[source];

            if (targetItem == null || targetItem.ItemData == null || !targetItem.ItemData.IsStackable ||
                sourceItem == null || sourceItem.ItemData == null || !sourceItem.ItemData.IsStackable ||
                !targetItem.ItemData.Equals(sourceItem.ItemData))
            {
                return false;
            }
            else
            {
                var newStack = targetItem.Stack + sourceItem.Stack;

                if (newStack <= targetItem.ItemData.MaxStack)
                {
                    targetItem.Stack = newStack;
                    _items[source] = null;
                }
                else
                {
                    targetItem.Stack = newStack;
                    sourceItem.Stack = Math.Abs(targetItem.ItemData.MaxStack - newStack);
                }
            }

            return false;
        }

        /// <summary>
        /// Reshuffle to move empty items to the end of the collection
        /// </summary>
        public void ReShuffle()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].Stack <= 0)
                {
                    _items[i] = null;
                }
            }
            _items = _items.OrderByDescending(x => x != null).ToArray();
        }

        private bool CanStack(int target, int source)
        {
            var targetItem = _items[target];
            var sourceItem = _items[source];

            if (targetItem == null || targetItem.ItemData == null || !targetItem.ItemData.IsStackable ||
                sourceItem == null || sourceItem.ItemData == null || !sourceItem.ItemData.IsStackable ||
                !targetItem.ItemData.Equals(sourceItem.ItemData))
            {
                return false;
            }
            else
            {
                return targetItem.Stack < targetItem.ItemData.MaxStack && sourceItem.Stack < sourceItem.ItemData.MaxStack;
            }
        }
    }
}