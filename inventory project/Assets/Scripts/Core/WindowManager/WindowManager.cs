using Cysharp.Threading.Tasks;
using MongrelsTeam.Data;
using MongrelsTeam.Interfaces;
using MongrelsTeam.UI;
using System.Collections.Generic;
using UnityEngine;

namespace MongrelsTeam.Service
{
    public class WindowManager : IService, IDisposable
    {
        private WindowStorage _windowStorage;
        private Transform _windowContainer;

        private Dictionary<WindowType, Window> _windows;

        public WindowManager(WindowStorage windowStorage, Transform windowContainer)
        {
            _windowStorage = windowStorage;
            _windowContainer = windowContainer;
            _windows = new Dictionary<WindowType, Window>();
        }

        public async UniTask Show<T>(WindowType windowType, IWindowArgs args = null) where T : Window
        {
            if (!_windows.ContainsKey(windowType))
            {
                var newWindow = Object.Instantiate(_windowStorage.GetWindow(windowType), _windowContainer);
                newWindow.gameObject.SetActive(false);
                await newWindow.Setup();
                await newWindow.Show(args);
                _windows.Add(windowType, newWindow);
            }
            else
            {
                await _windows[windowType].Show(args);
            }
        }

        public async UniTask Hide<T>(WindowType windowType) where T : Window
        {
            if (!_windows.ContainsKey(windowType))
            {
                Debug.LogError($"WINDOW NOT FIND: {windowType}");
                return;
            }

            await _windows[windowType].Hide();
        }

        public void Dispose()
        {
            foreach (var window in _windows.Values)
            {
                Object.Destroy(window);
            }
            _windows.Clear();
        }
    }
}
