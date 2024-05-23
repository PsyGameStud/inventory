using UnityEngine;

namespace MongrelsTeam.Core
{
    public abstract class EntryPoint : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Register();
    }
}