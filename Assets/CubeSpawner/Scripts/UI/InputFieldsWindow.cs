using CubeSpawner.Scripts.Logic;
using UnityEngine;

namespace CubeSpawner.Scripts.UI
{
    public class InputFieldsWindow : MonoBehaviour
    {
        [SerializeField] private ParameterInputField _speedInputField;
        [SerializeField] private ParameterInputField _distanceInputField;
        [SerializeField] private ParameterInputField _intervalInputField;

        private ICubeSpawner _cubeSpawner;

        private void OnDestroy()
        {
            _speedInputField.OnParameterChanged -= SpeedParameterChanged;
            _distanceInputField.OnParameterChanged -= DistanceParameterChanged;
            _intervalInputField.OnParameterChanged -= IntervalParameterChanged;
        }

        public void Construct(ICubeSpawner cubeSpawner, float startSpeed, float startDistance, float startInterval)
        {
            _cubeSpawner = cubeSpawner;

            _speedInputField.SetStartValue(startSpeed);
            _speedInputField.OnParameterChanged += SpeedParameterChanged;

            _distanceInputField.SetStartValue(startDistance);
            _distanceInputField.OnParameterChanged += DistanceParameterChanged;

            _intervalInputField.SetStartValue(startInterval);
            _intervalInputField.OnParameterChanged += IntervalParameterChanged;
        }

        private void SpeedParameterChanged(float value) =>
            _cubeSpawner.CubeSpeed = value;

        private void DistanceParameterChanged(float value) =>
            _cubeSpawner.Distance = value;

        private void IntervalParameterChanged(float value) =>
            _cubeSpawner.SpawnInterval = value;
    }
}