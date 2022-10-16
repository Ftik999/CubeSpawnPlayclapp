using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private const float MinimalDistance = 0.01f;
    public event Action<Cube> Disable;

    private Vector3 _direction;

    private float _speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, _direction) <= MinimalDistance)
            DisableObject();
    }

    public void StartMove(Transform parent, Vector3 direction, float speed)
    {
        transform.position = parent.position;
        _direction = direction;
        _speed = speed;
    }

    private void DisableObject() => 
        Disable?.Invoke(this);
}