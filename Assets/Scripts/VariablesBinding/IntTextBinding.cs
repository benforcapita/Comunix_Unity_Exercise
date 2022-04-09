using System;
using Core.References;
using TMPro;
using UnityEngine;

namespace VariablesBinding
{
    public class IntTextBinding : MonoBehaviour
    {
        [SerializeField] private IntVariable _intVariable;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _intVariable.onVariableChanged+=IntVariableOnVariableChanged;
            _text.text = _intVariable.runtimeValue.ToString();
        }

        private void IntVariableOnVariableChanged(object sender, VariableChangedEventArgs<int> args)
        {
            _text.text = args.newValue.ToString();
        }

        private void OnDisable()
        {
            _intVariable.onVariableChanged-=IntVariableOnVariableChanged;
        }
    }
}
