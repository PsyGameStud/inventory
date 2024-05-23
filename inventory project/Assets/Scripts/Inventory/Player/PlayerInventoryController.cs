using MongrelsTeam.Service;
using MongrelsTeam.Signals;
using UnityEngine;

namespace MongrelsTeam.Game
{
    public class PlayerInventoryController : MonoBehaviour
    {
        [SerializeField] private GameObject _hat;
        [SerializeField] private GameObject _back;

        private Inventory _inventory;
        private EventBus _eventBus;

        private void Start()
        {
            _inventory = ServiceLocator.Current.Get<Inventory>();
            _eventBus = ServiceLocator.Current.Get<EventBus>();

            _inventory.OnDrop += DropItems;
            _eventBus.Subscribe<PlayerEquippedSignal>(EquipItem);
        }

        public bool AddItem(Item item)
        {
            return _inventory.AddItem(item);
        }

        private void EquipItem(PlayerEquippedSignal signal)
        {
            switch (signal.EquipItem)
            {
                case "Hat":
                    _hat.SetActive(signal.IsEquiped);
                    break;
                case "Back":
                    _back.SetActive(signal.IsEquiped);
                    break;
            }
        }

        private void DropItems(Item item)
        {
            for (int i = 0; i < item.CurrentStack; i++)
            {
                Vector3 dropPosition = transform.position + Random.insideUnitSphere * 4;
                dropPosition.y = 0.5f;

                Instantiate(item.ItemData.PickupPrefab, dropPosition, Quaternion.identity);
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerEquippedSignal>(EquipItem);
            _inventory.OnDrop -= DropItems;
        }
    }
}
