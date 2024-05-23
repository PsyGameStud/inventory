using MongrelsTeam.Game;
using MongrelsTeam.Service;
using System.Collections.Generic;
using UnityEngine;

namespace MongrelsTeam.UI
{
    public class InventorySlotsWidget : MonoBehaviour
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;

        private List<InventorySlot> _slots = new List<InventorySlot>();

        public void Setup()
        {
            var inventory = ServiceLocator.Current.Get<Inventory>();

            for (int i = 0; i < inventory.SlotCount; i++)
            {
                var newSlot = Instantiate(_inventorySlotPrefab, transform);
                _slots.Add(newSlot);
            }
        }

        public void UpdateSlots()
        {
            var inventory = ServiceLocator.Current.Get<Inventory>();
            var items = inventory.GetAllItems();
            int slotIndex = 0;

            foreach (var itemList in items.Values)
            {
                foreach (var item in itemList)
                {
                    if (slotIndex < _slots.Count)
                    {
                        _slots[slotIndex].SetItem(item);
                        slotIndex++;
                    }
                }
            }
        }
    }
}
