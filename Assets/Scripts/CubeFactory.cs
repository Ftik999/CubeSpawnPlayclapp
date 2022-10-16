using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class CubeFactory:ICubeFactory
{
    private readonly ObjectPool<Cube> _cubePool;
    private readonly Cube _cubePrefab;

    public CubeFactory(Cube cubePrefab)
    {
        _cubePrefab = cubePrefab;
        _cubePool = new ObjectPool<Cube>(CreateCube, OnTakeFromPool, OnReleaseToPool, OnDestroyCube, false);
    }

    public void SpawnCube(Transform parent, Vector3 direction, float speed)
    {
        Cube cube = _cubePool.Get();
        cube.transform.SetParent(parent, true);
        cube.StartMove(parent, direction, speed);
    }

    private Cube CreateCube()
    {
        Cube cube = Object.Instantiate(_cubePrefab);
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
}