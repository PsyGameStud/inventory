using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using MongrelsTeam.UI;

namespace MongrelsTeam.Data
{
    [CreateAssetMenu(fileName = "window_storage", menuName = "Create Data / Window Storage")]
    public class WindowStorage : ScriptableObject
    {
        [SerializeField] private SerializableDictionaryBase<WindowType, Window> _windows;

        public Window GetWindow(WindowType windowType)
        {
            if (!_windows.ContainsKey(windowType))
            {
                Debug.LogError("WINDOW NOT FIND");
                return null;
            }

            return _windows[windowType];
        }
    }

    public enum WindowType
    {
        Inventory
    }
}
