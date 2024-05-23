using MongrelsTeam.Game;
using MongrelsTeam.Service;
using MongrelsTeam.Signals;
using UnityEngine;

namespace MongrelsTeam.UI
{
    public class EquipmentWidget : MonoBehaviour
    {
        [SerializeField] private EquipedSlot _hatSlot;
        [SerializeField] private EquipedSlot _backSlot;

        private EventBus _eventBus;
        private Inventory _inventory;

        public void Setup()
        {
            _inventory = ServiceLocator.Current.Get<Inventory>();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<PlayerEquippedSignal>(OnEquip);
        }

        private void OnEquip(PlayerEquippedSignal signal)
        {
            if (signal.IsEquiped && signal.IsUsed)
            {
                switch (signal.EquipItem)
                {
                    case "Hat":
                        _hatSlot.UpdateSlot(_inventory.GetItem(signal.EquipItem));
                        break;
                    case "Back":
                        _backSlot.UpdateSlot(_inventory.GetItem(signal.EquipItem));
                        break;
                }
            }
        }
    }
}
