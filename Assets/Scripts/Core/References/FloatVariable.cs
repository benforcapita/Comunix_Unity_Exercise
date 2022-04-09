using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.References
{
    [CreateAssetMenu(menuName = "Core/References/Float Variable")]
    public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        [ShowInInspector, ReadOnly]
        private                  float _runtimeValue;
        [SerializeField] private float initialValue;

        public float runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (Math.Abs(value - _runtimeValue) < float.Epsilon) return;
                var oldValue = runtimeValue;
                var newValue = value;
                _runtimeValue = value;
                onVariableChanged?.Invoke(this, new VariableChangedEventArgs<float>(oldValue, newValue));
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }

        public void SetWithoutUpdate(float newValue)
        {
            _runtimeValue = newValue;
        }

        public event VariableChangedEvent<float> onVariableChanged;

#if UNITY_EDITOR
        [SerializeField] private float updateValue;

        [Button]
        public void SetValue()
        {
            runtimeValue = updateValue;
        }
#endif
    }
}
