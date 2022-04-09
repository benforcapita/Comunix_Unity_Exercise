using System;
using UnityEngine;

namespace Core.References
{
    [CreateAssetMenu(menuName = "Core/References/Object Variable")]
    public class ObjectVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        private                  object _runtimeValue;
        [SerializeField] private object initialValue;

        public object runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (value == _runtimeValue) return;
                _runtimeValue = value;
                Updated();
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }


        public event Action onVariableChanged;

#if UNITY_EDITOR
        public void SetValue(object newValue)
        {
            runtimeValue = newValue;
        }
#endif

        public void Updated()
        {
            onVariableChanged?.Invoke();
        }
    }
}

