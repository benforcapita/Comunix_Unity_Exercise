using System;
using Core.References;
using Sirenix.OdinInspector;
using UnityEngine;

    [CreateAssetMenu(menuName = "Core/References/DateTime Variable")]
    public class DateTimeOffsetVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        private                  DateTimeOffset _runtimeValue;
        [SerializeField] private int            initialSecondsAgo = 0;

        public DateTimeOffset runtimeValue
        {
            get => _runtimeValue;
            set
            {
                if (value == _runtimeValue) return;
                var oldValue = runtimeValue;
                var newValue = value;
                _runtimeValue = value;
                onVariableChanged?.Invoke(this, new VariableChangedEventArgs<DateTimeOffset>(oldValue, newValue));
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = DateTimeOffset.Now - TimeSpan.FromSeconds(initialSecondsAgo);
        }


        public event VariableChangedEvent<DateTimeOffset> onVariableChanged;

#if UNITY_EDITOR
        [SerializeField] private int secondsAgo = 0;

        [Button]
        public void SetValue()
        {
            runtimeValue = DateTimeOffset.Now - TimeSpan.FromSeconds(secondsAgo);
        }
#endif
    }
