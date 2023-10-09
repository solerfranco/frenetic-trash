using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private int _failSafeCounter;

    private int _spawnCount;
    [SerializeField]
    private int _maxSpawnAmount;
    [SerializeField]
    private Obstacle[] _obstaclePrefabs;

    [SerializeField]
    private float _raycastRadius;

    [SerializeField]
    private Collider2D _confinerCollider;

    private void Start()
    {
        while (_spawnCount < _maxSpawnAmount && _failSafeCounter < _maxSpawnAmount * 10)
        {
            Obstacle obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)], GetRandomPositionConfined(), Quaternion.identity);
            obstacle._raycastRadius = _raycastRadius;
            if (obstacle.CheckForColliders())
            {
                Destroy(obstacle.gameObject);
            }else
            {
                _spawnCount++;
            }
            _failSafeCounter++;
        }
    }

    private Vector2 GetRandomPositionConfined()
    {
        Vector3 bottomLeft = _confinerCollider.bounds.min;
        Vector3 topRight = _confinerCollider.bounds.max;

        Vector2 randPos = new(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
        return randPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _raycastRadius);
    }
}
