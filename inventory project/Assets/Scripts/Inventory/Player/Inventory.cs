using MongrelsTeam.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MongrelsTeam.Game
{
    public class Inventory : IService
    {
        private readonly Dictionary<string, List<Item>> _items = new Dictionary<string, List<Item>>();

        public int SlotCount { get; private set; }
        public event Action<Item> OnDrop;


        public Inventory(int slotCount)
        {
            SlotCount = slotCount;
        }

        public bool AddItem(Item newItem)
        {
            // Проверка на переполнение инвентаря по количеству ячеек
            int totalItems = 0;
            foreach (var list in _items.Values)
            {
                totalItems += list.Count;
            }

            if (totalItems >= SlotCount && (!_items.ContainsKey(newItem.ItemData.ItemName) || (_items.ContainsKey(newItem.ItemData.ItemName) && _items[newItem.ItemData.ItemName].Count >= newItem.ItemData.MaxStack)))
            {
                DropItem(newItem);
                return false;
            }

            // Проверка на переполнение стека предметов
            if (!_items.ContainsKey(newItem.ItemData.ItemName))
            {
                _items[newItem.ItemData.ItemName] = new List<Item>();
            }

            List<Item> itemList = _items[newItem.ItemData.ItemName];

            foreach (Item item in itemList)
            {
                if (item.CurrentStack < item.ItemData.MaxStack)
                {
                    int stackSpace = item.ItemData.MaxStack - item.CurrentStack;

                    if (newItem.CurrentStack <= stackSpace)
                    {
                        item.CurrentStack += newItem.CurrentStack;
                        return true;
                    }
                    else
                    {
                        item.CurrentStack = item.ItemData.MaxStack;
                        newItem.CurrentStack -= stackSpace;
                    }
                }
            }

            // Проверка на заполнение ячеек в списке предметов
            if (totalItems < SlotCount)
            {
                itemList.Add(newItem);
                return true;
            }

            DropItem(newItem);
            return false;
        }

        public void DropItem(Item item)
        {
            OnDrop?.Invoke(item);
            RemoveItem(item.ItemData.ItemName, item.CurrentStack);
        }

        public void RemoveItem(string itemName, int amount)
        {
            if (_items.ContainsKey(itemName))
            {
                List<Item> itemList = _items[itemName];
                for (int i = itemList.Count - 1; i >= 0; i--)
                {
                    if (itemList[i].CurrentStack > amount)
                    {
                        itemList[i].CurrentStack -= amount;
                        return;
                    }
                    else
                    {
                        amount -= itemList[i].CurrentStack;
                        itemList.RemoveAt(i);
                    }
                }
                if (itemList.Count == 0)
                {
                    _items.Remove(itemName);
                }
            }
        }

        public Item GetItem(string itemName)
        {
            if (_items.ContainsKey(itemName) && _items[itemName].Count > 0)
            {
                return _items[itemName][0];
            }

            return null;
        }

        public Dictionary<string, List<Item>> GetAllItems()
        {
            return _items;
        }
    }
}
