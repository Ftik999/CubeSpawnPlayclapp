using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _distance = 10;
    [SerializeField] private float _cubeSpeed = 5;
    [SerializeField] private float _spawnInterval = 1;

    private ICubeFactory _cubeFactory;

    private float _timeToSpawn;
    private void Awake()
    {
        _cubeFactory = new CubeFactory(_cubePrefab);
    }

    private void Update()
    {
        if (_timeToSpawn < Time.time)
        {
            _cubeFactory.SpawnCube(transform, GetCubeDirection(), _cubeSpeed);
            _timeToSpawn = Time.time + _spawnInterval;
        }
    }

    private Vector3 GetCubeDirection() => 
        Random.onUnitSphere * _distance;
}
