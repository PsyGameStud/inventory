using MongrelsTeam.Game;
using MongrelsTeam.Service;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MongrelsTeam.UI
{
    public class InventorySlot : BaseSlot, IDropHandler
    {
        [SerializeField] private ItemUI _itemUIPrefab;

        private ItemUI _itemUI;
        private Item _item;

        public override bool IsOccupied => _item != null;

        public override void SetItem(Item item)
        {
            if (IsOccupied) return;

            _item = item;
            _itemUI = Instantiate(_itemUIPrefab, transform);
            _itemUI.Setup(item, this);

            var inventory = ServiceLocator.Current.Get<Inventory>();

            if (inventory.GetItem(_item.ItemData.ItemName) == null)
            {
                inventory.AddItem(_item);
            }
        }

        public override void ClearSlot()
        {
            if (_itemUI != null)
            {
                Destroy(_itemUI.gameObject);
            }

            _item = null;
            _itemUI = null;
        }

        public Item GetItem()
        {
            return _item;
        }

        public void OnDrop(PointerEventData eventData)
        {
            ItemUI droppedItemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            if (droppedItemUI != null && !IsOccupied)
            {
                InventorySlot originalSlot = droppedItemUI.GetComponentInParent<InventorySlot>();
                if (originalSlot != null)
                {
                    Item item = originalSlot.GetItem();
                    originalSlot.ClearSlot();
                    SetItem(item);
                }
            }
        }
    }
}