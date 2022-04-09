using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.References
{
    [CreateAssetMenu(menuName = "Core/References/Timespan Variable")]
    public class TimespanVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        
        private                  TimeSpan _runtimeValue;
        [SerializeField] private int      initialSeconds;

        [ShowInInspector, ReadOnly]
        private string runtimeString;

        public TimeSpan runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (value == _runtimeValue) return;
                var oldValue = runtimeValue;
                var newValue = value;
                _runtimeValue = value;
                onVariableChanged?.Invoke(this, new VariableChangedEventArgs<TimeSpan>(oldValue, newValue));
                runtimeString = _runtimeValue.ToString();
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = TimeSpan.FromSeconds(initialSeconds);
        }


        public event VariableChangedEvent<TimeSpan> onVariableChanged;

#if UNITY_EDITOR
        [SerializeField] private int seconds = 0;

        [Button]
        public void SetValue()
        {
            runtimeValue = TimeSpan.FromSeconds(seconds);
        }
#endif
    }
}
