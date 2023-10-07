using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPowerUps : MonoBehaviour
{
    [Header("Power Up Params")]

    [Tooltip("The speed at which the trash flies towards the magnet")]
    [SerializeField]
    private float _magnetSpeed;

    [Range(2f, 10f)]
    [SerializeField]
    private float _magnetRadius;

    [SerializeField]
    private float _magnetDuration;

    public bool hasMagnet;
    [Space]
    [Header("Set up")]
    [SerializeField]
    private LayerMask _trashLayer;

    [SerializeField]
    private SpriteRenderer _magnetRenderer;
    void Update()
    {
        if (hasMagnet)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _magnetRadius, _trashLayer);
            float step = _magnetSpeed * Time.deltaTime;

            foreach (var trash in colliders)
            {
                trash.transform.position = Vector2.MoveTowards(trash.transform.position, transform.position, step);
            }
        }
    }

    public void EnableMagnet()
    {
        StopAllCoroutines();
        _magnetRenderer.enabled = true;
        hasMagnet = true;
        StartCoroutine(DisableMagnet());
    }

    private IEnumerator DisableMagnet()
    {
        yield return new WaitForSeconds(_magnetDuration);
        hasMagnet = false;
        _magnetRenderer.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _magnetRadius);
    }
}
