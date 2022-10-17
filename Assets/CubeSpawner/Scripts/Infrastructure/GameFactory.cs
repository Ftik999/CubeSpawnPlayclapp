using CubeSpawner.Scripts.Logic;
using CubeSpawner.Scripts.UI;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace CubeSpawner.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string CubePath = "Cube";
        private const string UIInputFieldsWindowPath = "UI/InputFieldsWindow";

        private readonly ObjectPool<Cube> _cubePool;

        public GameFactory()
        {
            _cubePool = new ObjectPool<Cube>(CreateCube, OnTakeFromPool, OnReleaseToPool, OnDestroyCube, false);
        }

        public void CreateUI(ICubeSpawner cubeSpawner, float startSpeed, float startDistance, float startInterval)
        {
            GameObject uiRoot = Instantiate(UIInputFieldsWindowPath);
            uiRoot.GetComponentInChildren<InputFieldsWindow>()
                .Construct(cubeSpawner, startSpeed, startDistance, startInterval);
        }

        public void SpawnCube(Transform parent, Vector3 direction, float speed)
        {
            Cube cube = _cubePool.Get();
            cube.transform.SetParent(parent, true);
            cube.StartMove(parent, direction, speed);
        }

        private Cube CreateCube()
        {
            Cube cube = Instantiate(CubePath).GetComponent<Cube>();
            cube.Disable += ReturnCubeToPool;
            cube.gameObject.SetActive(false);

            return cube;
        }

        private void ReturnCubeToPool(Cube cube) =>
            _cubePool.Release(cube);

        private void OnTakeFromPool(Cube cube) =>
            cube.gameObject.SetActive(true);

        private void OnReleaseToPool(Cube cube) =>
            cube.gameObject.SetActive(false);

        private void OnDestroyCube(Cube cube) =>
            Object.Destroy(cube.gameObject);

        private GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}