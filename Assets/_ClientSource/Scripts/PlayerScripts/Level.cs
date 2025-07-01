using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace ROAMMO
{
    [DisallowMultipleComponent]
    public class Level : NetworkBehaviour
    {
        // Use SyncVar<T> instead of [SyncVar]
        public readonly SyncVar<int> current = new();
        [SerializeField]public int max = 1;

        protected override void OnValidate()
        {
            base.OnValidate();

            // Clamp and assign the value using .Value
            current.Value = Mathf.Clamp(current.Value, 0, max);
        }
    }
}
