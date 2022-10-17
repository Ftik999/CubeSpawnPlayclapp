using CubeSpawner.Scripts.Logic;
using UnityEngine;

namespace CubeSpawner.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        void CreateUI(ICubeSpawner cubeSpawner, float startSpeed, float startDistance, float startInterval);
        void SpawnCube(Transform parent, Vector3 direction, float speed);
    }
}