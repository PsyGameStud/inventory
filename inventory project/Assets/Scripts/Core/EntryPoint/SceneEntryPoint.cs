using MongrelsTeam.Data;
using MongrelsTeam.Game;
using MongrelsTeam.Service;
using UnityEngine;

namespace MongrelsTeam.Core
{
    public class SceneEntryPoint : EntryPoint
    {
        [SerializeField] private Transform _windowContainer;
        [SerializeField] private WindowStorage _windowStorage;

        private Inventory _inventory;
        private EventBus _eventBus;
        private WindowManager _windowManager;

        private void Awake()
        {
            _windowManager = new WindowManager(_windowStorage, _windowContainer);
            _eventBus = new EventBus();
            _inventory = new Inventory(3);

            Init();
            Register();
        }

        public override void Init()
        {
            ServiceLocator.Initialize();

        }

        public override void Register()
        {
            ServiceLocator.Current.Register(_windowManager);
            ServiceLocator.Current.Register(_eventBus);
            ServiceLocator.Current.Register(_inventory);
        }
    }
}
