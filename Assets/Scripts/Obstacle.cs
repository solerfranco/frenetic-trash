using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [HideInInspector]
    public float _raycastRadius;
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
