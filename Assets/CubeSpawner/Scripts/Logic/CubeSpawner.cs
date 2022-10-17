using CubeSpawner.Scripts.Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeSpawner.Scripts.Logic
{
    public class CubeSpawner : MonoBehaviour, ICubeSpawner
    {
        [SerializeField] private float _distance = 10;
        [SerializeField] private float _cubeSpeed = 5;
        [SerializeField] private float _spawnInterval = 1;

        private float _timeToSpawn;
        
        private IGameFactory _gameFactory;

        public float Distance
        {
            set
            {
                if (_distance == value)
                {
                    return;
                }

                _distance = value;
            }
        }

        public float CubeSpeed
        {
            set
            {
                if (_cubeSpeed == value)
                {
                    return;
                }

                _cubeSpeed = value;
            }
        }

        public float SpawnInterval
        {
            set
            {
                if (_spawnInterval == value)
                {
                    return;
                }

                _spawnInterval = value;
            }
        }

        private void Awake()
        {
            _gameFactory = new GameFactory();
            _gameFactory.CreateUI(this, _cubeSpeed, _distance, _spawnInterval);
        }

        private void Update()
        {
            if (_timeToSpawn < Time.time)
            {
                _gameFactory.SpawnCube(transform, GetCubeDirection(), _cubeSpeed);
                _timeToSpawn = Time.time + _spawnInterval;
            }
        }

        private Vector3 GetCubeDirection() =>
            Random.onUnitSphere * _distance;
    }
}