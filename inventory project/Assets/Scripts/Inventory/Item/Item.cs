using MongrelsTeam.Data;

namespace MongrelsTeam.Game
{
    public class Item
    {
        public ItemData ItemData { get; private set; }
        public int CurrentStack { get; set; }

        public Item(ItemData data, int stack = 1)
        {
            ItemData = data;
            CurrentStack = stack;
        }

        public virtual void Use()
        {
        }
    }
}