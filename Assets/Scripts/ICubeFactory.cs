using UnityEngine;

public interface ICubeFactory
{
    void SpawnCube(Transform parent, Vector3 direction, float speed);
}