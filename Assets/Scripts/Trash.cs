using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [HideInInspector]
    public Transform Target;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Sprite[] _sprites;

    [HideInInspector]
    public float _raycastRadius;

    private void Start()
    {
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
    private void Update()
    {
        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, _speed * Time.deltaTime);
        }
    }

    public bool CheckForColliders()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _raycastRadius);

        if (colliders.Length > 2)
        {
            return true;
        }
        return false;
    }
}
