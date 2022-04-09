using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.References
{
    [CreateAssetMenu(menuName = "Core/References/Boolean Variable")]
    public class BooleanVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        [ShowInInspector, Sirenix.OdinInspector.ReadOnly]
        private                  bool _runtimeValue;
        [SerializeField] private bool initialValue;

        public bool runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (value == runtimeValue) return;
                var oldValue = runtimeValue;
                var newValue = value;
                _runtimeValue = value;
                onVariableChanged?.Invoke(this, new VariableChangedEventArgs<bool>(oldValue, newValue));
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }


        public event VariableChangedEvent<bool> onVariableChanged;

#if UNITY_EDITOR
        [SerializeField] private bool updateValue;

        [Button]
        public void SetValue()
        {
            runtimeValue = updateValue;
        }
#endif
    }
}
