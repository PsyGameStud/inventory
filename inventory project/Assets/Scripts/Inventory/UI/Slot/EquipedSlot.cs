using MongrelsTeam.Game;
using MongrelsTeam.Service;
using MongrelsTeam.Signals;
using UnityEngine;

namespace MongrelsTeam.UI
{
    public class EquipedSlot : BaseSlot
    {
        [SerializeField] private ItemUI _itemUIPrefab;
        [SerializeField] private string _slotId;

        public string SlotId => _slotId;

        private ItemUI _itemUI;
        private Item _item;

        public override bool IsOccupied => _item != null;

        public void UpdateSlot(Item item)
        {
            _itemUI = Instantiate(_itemUIPrefab, transform);
            _item = item;
            _itemUI.Setup(item, this, false);
        }

        public override void SetItem(Item item)
        {
            if (IsOccupied || !item.ItemData.IsEquiped) return;

            var inventory = ServiceLocator.Current.Get<Inventory>();
            inventory.RemoveItem(item.ItemData.ItemName, 1);

            ServiceLocator.Current.Get<EventBus>().SendSignal(new PlayerEquippedSignal(item.ItemData.ItemName, true, false));

            _itemUI = Instantiate(_itemUIPrefab, transform);
            _item = item;
            _itemUI.Setup(item, this, false);
        }

        public override void ClearSlot()
        {
            if (_itemUI != null)
            {
                ServiceLocator.Current.Get<EventBus>().SendSignal(new PlayerEquippedSignal(_item.ItemData.ItemName, false, false));

                Destroy(_itemUI.gameObject);
            }
            _item = null;
        }
    }
}
