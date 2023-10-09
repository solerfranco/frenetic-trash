using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField]
    private int _powerUpSpawnRate;
    [SerializeField]
    private int _maxSpawnAmount;
    private int _spawnCount;
    private Camera _camera;
    [SerializeField]
    private Trash _trashPrefab;
    [SerializeField]
    private GameObject[] _powerUpPrefabs;

    [SerializeField]
    private float _raycastRadius;

    [SerializeField]
    private float delayBetweenSpawns;

    [SerializeField]
    private Collider2D _confinerCollider;
    private void Awake()
    {
        _camera = Camera.main;
    }

    private IEnumerator Start()
    {

        while (true)
        {
            _spawnCount++;
            if (_spawnCount % _powerUpSpawnRate == 0)
            {
                Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Length)], GetRandomPositionConfined(), Quaternion.identity);
            }
            if(FindObjectsOfType<Trash>().Length < _maxSpawnAmount)
            {
                int failSafeCounter = 0;
                Trash trash = Instantiate(_trashPrefab, GetRandomPositionConfined(), Quaternion.identity);
                trash._raycastRadius = _raycastRadius;
                while (trash.CheckForColliders() && failSafeCounter < 15)
                {
                    trash.transform.position = GetRandomPositionConfined();
                    failSafeCounter++;
                }
            }

            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    private Vector2 GetRandomPositionConfined()
    {
        Vector3 bottomLeft = _confinerCollider.bounds.min;
        Vector3 topRight = _confinerCollider.bounds.max;

        Vector2 randPos;
        do
        {
            randPos = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
        } while (IsPointInsideCamara(randPos));
        return randPos;
    }
    private bool IsPointInsideCamara(Vector2 pos)
    {
        Vector2 viewportPos = _camera.WorldToViewportPoint(pos);
        return viewportPos.x >= 0 && viewportPos.y >= 0 && viewportPos.x <= 1 && viewportPos.y <= 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _raycastRadius);
    }
}
