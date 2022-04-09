using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.References
{
    [CreateAssetMenu(menuName = "Core/References/String Variable")]
    public class StringVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        [ShowInInspector, ReadOnly]
        private string _runtimeValue;
        [SerializeField] private string initialValue;

        public string runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (value.Equals(runtimeValue)) return;
                var oldValue = runtimeValue;
                var newValue = value;
                _runtimeValue = value;
                onVariableChanged?.Invoke(this, new VariableChangedEventArgs<string>(oldValue, newValue));
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }


        public event VariableChangedEvent<string> onVariableChanged;

#if UNITY_EDITOR
        [SerializeField] private string updateValue;

        [Button]
        public void SetValue()
        {
            runtimeValue = updateValue;
        }
#endif
    }
}
