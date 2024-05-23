using MongrelsTeam.Game;
using UnityEngine;

namespace MongrelsTeam.UI
{
    public abstract class BaseSlot : MonoBehaviour
    {
        public abstract void SetItem(Item item);
        public abstract void ClearSlot();
        public abstract bool IsOccupied { get; }
    }
}
