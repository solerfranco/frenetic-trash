using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [HideInInspector]
    public Transform Target;
    [SerializeField]
    private float speed;

    private void Update()
    {
        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        }
    }
}
