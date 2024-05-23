using MongrelsTeam.Data;
using UnityEngine;

namespace MongrelsTeam.Game
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private ItemType _itemType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventoryController controller))
            {
                var newItem = GetItem();

                if (controller.AddItem(newItem))
                {
                    Destroy(gameObject);
                }
            }
        }

        private Item GetItem()
        {
            switch (_itemType)
            {
                case ItemType.Health:
                    return new HealthPotionItem(_itemData);
                case ItemType.Hat:
                    return new HatItem(_itemData);
                case ItemType.Back:
                    return new BackItem(_itemData);
            }

            return null;
        }
    }

    public enum ItemType
    {
        Health,
        Hat,
        Back
    }
}
