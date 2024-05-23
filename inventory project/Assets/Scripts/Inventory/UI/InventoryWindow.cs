using Cysharp.Threading.Tasks;
using MongrelsTeam.Interfaces;
using UnityEngine;

namespace MongrelsTeam.UI
{
    public class InventoryWindow : Window
    {
        [SerializeField] private InventorySlotsWidget _inventorySlotsWidget;
        [SerializeField] private EquipmentWidget _equipmentWidget;

        public override async UniTask Setup()
        {
            await base.Setup();

            _inventorySlotsWidget.Setup();
            _equipmentWidget.Setup();
        }

        public override async UniTask Show(IWindowArgs args = null)
        {
            await base.Show(args);
            _inventorySlotsWidget.UpdateSlots();
        }
    }
}
