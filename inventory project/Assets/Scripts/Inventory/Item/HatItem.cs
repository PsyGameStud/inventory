using MongrelsTeam.Data;
using MongrelsTeam.Service;
using MongrelsTeam.Signals;

namespace MongrelsTeam.Game
{
    public class HatItem : Item
    {
        public HatItem(ItemData data, int stack = 1) : base(data, stack)
        {
        }

        public override void Use()
        {
            ServiceLocator.Current.Get<EventBus>().SendSignal(new PlayerEquippedSignal(this.ItemData.ItemName, true, true));
        }
    }
}
