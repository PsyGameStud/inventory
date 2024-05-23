namespace MongrelsTeam.Signals 
{ 
    public class PlayerEquippedSignal
    {
        public string EquipItem { get; private set; }
        public bool IsEquiped { get; private set; }
        public bool IsUsed { get; private set; }

        public PlayerEquippedSignal(string equipItem, bool isEquiped, bool isUsed)
        {
            IsEquiped = isEquiped;
            EquipItem = equipItem;
            IsUsed = isUsed;
        }
    }
}
