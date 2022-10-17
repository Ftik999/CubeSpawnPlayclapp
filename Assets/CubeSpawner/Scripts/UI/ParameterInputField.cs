using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CubeSpawner.Scripts.UI
{
    public class ParameterInputField : MonoBehaviour
    {
        public event Action<float> OnParameterChanged;

        [SerializeField] private TMP_InputField _inputField;

        private ColorBlock _colorBlock;
        private Color _startSelectedColor;
        private Color _corectResultColor = new(0, 0.482f, 0.137f);
        private Color _wrongResultColor = new(0.726f, 0.071f, 0.072f);
        private float _currentParameters;

        private void Start()
        {
            _inputField.onValueChanged.AddListener(InputValueChanged);
            _inputField.onDeselect.AddListener(DeselectField);

            _colorBlock = _inputField.colors;
            _startSelectedColor = _colorBlock.selectedColor;
        }

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(InputValueChanged);
            _inputField.onDeselect.RemoveListener(DeselectField);
        }

        public void SetStartValue(float startValue)
        {
            _currentParameters = startValue;
            _inputField.text = _currentParameters + "";
        }

        private void InputValueChanged(string text)
        {
            if (IsNumbers(text, out float result))
            {
                if (result > 0)
                {
                    _colorBlock.selectedColor = _corectResultColor;
                    _currentParameters = result;
                    OnParameterChanged?.Invoke(_currentParameters);
                }
                else
                {
                    _colorBlock.selectedColor = _wrongResultColor;
                }

                _inputField.colors = _colorBlock;
            }
        }

        private void DeselectField(string text)
        {
            _inputField.text = _currentParameters + "";
            _colorBlock.selectedColor = _startSelectedColor;
            _inputField.colors = _colorBlock;
        }

        private static bool IsNumbers(string text, out float result) =>
            float.TryParse(text, out result);
    }
}