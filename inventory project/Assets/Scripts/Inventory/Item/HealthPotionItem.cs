using MongrelsTeam.Data;
using UnityEngine;

namespace MongrelsTeam.Game
{
    public class HealthPotionItem : Item
    {
        public HealthPotionItem(ItemData data, int stack = 1) : base(data, stack)
        {
        }

        public override void Use()
        {
            Debug.LogError("HEAL");
        }
    }
}
