using UnityEngine;

namespace MongrelsTeam.Data
{
    [CreateAssetMenu(fileName = "item_data", menuName = "Create Data / Item Data")]
    public class ItemData : ScriptableObject
    {
        public string ItemName;
        public bool IsConsumable;
        public bool IsEquiped;
        public int MaxStack;
        public Sprite Icon;
        public GameObject PickupPrefab;
    }
}
