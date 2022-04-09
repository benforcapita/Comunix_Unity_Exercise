using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.References
{
    [CreateAssetMenu(menuName = "Core/References/Int Variable")]
    public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        [ShowInInspector, ReadOnly]
        private                  int _runtimeValue;
        [SerializeField] private int initialValue;

        public int runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (value == _runtimeValue) return;
                var oldValue = runtimeValue;
                var newValue = value;
                _runtimeValue = value;
                onVariableChanged?.Invoke(this, new VariableChangedEventArgs<int>(oldValue, newValue));
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }


        public event VariableChangedEvent<int> onVariableChanged;

#if UNITY_EDITOR
        [SerializeField] private int updateValue;

        [Button]
        public void SetValue()
        {
            runtimeValue = updateValue;
        }
#endif
    }
}
