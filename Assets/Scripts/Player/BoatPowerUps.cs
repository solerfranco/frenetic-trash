using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPowerUps : MonoBehaviour
{
    [Header("Power Up Params")]

    [Space]
    [Header("Magnet Params")]

    [Range(2f, 10f)]
    [SerializeField]
    private float _magnetRadius;

    [SerializeField]
    private float _magnetDuration;

    public bool hasMagnet;

    [Space]
    [Header("Speed Params")]
    [SerializeField]
    private float _superSpeedAcceleration;
    [SerializeField]
    private float _superSpeedTorque;
    [SerializeField]
    private float _superSpeedDuration;

    [Space]
    [Header("Set up")]
    [SerializeField]
    private LayerMask _trashLayer;
    private BoatMovement _boatMovement;
    [SerializeField]
    private Animator _playerAnim;

    [SerializeField]
    private SpriteRenderer _magnetRenderer;

    private float _baseSpeed;
    private float _baseTorque;
    private Coroutine _magnetCoroutine;
    private Coroutine _superSpeedCoroutine;

    private void Awake()
    {
        _boatMovement = GetComponent<BoatMovement>();
    }
    void Update()
    {
        if (hasMagnet)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _magnetRadius, _trashLayer);

            foreach (var trash in colliders)
            {
                trash.GetComponent<Trash>().Target = transform;
            }
        }
    }

    public void EnableMagnet()
    {
        if (_magnetCoroutine != null) StopCoroutine(_magnetCoroutine);
        _magnetRenderer.enabled = true;
        hasMagnet = true;
        _magnetCoroutine = StartCoroutine(DisableMagnet());
    }
    private IEnumerator DisableMagnet()
    {
        yield return new WaitForSeconds(_magnetDuration);
        hasMagnet = false;
        _magnetRenderer.enabled = false;
    }

    public void EnableSuperSpeed()
    {
        if (_superSpeedCoroutine != null) StopCoroutine(_superSpeedCoroutine);
        _playerAnim.SetBool("Dash", true);
        _baseSpeed = _boatMovement.Acceleration;
        _baseTorque = _boatMovement.Torque;
        _boatMovement.Acceleration = _superSpeedAcceleration;
        _boatMovement.Torque = _superSpeedTorque;
        _superSpeedCoroutine = StartCoroutine(DisableSuperSpeed());
    }

    private IEnumerator DisableSuperSpeed()
    {
        yield return new WaitForSeconds(_superSpeedDuration);
        _playerAnim.SetBool("Dash", false);
        _boatMovement.Acceleration = _baseSpeed;
        _boatMovement.Torque = _baseTorque;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _magnetRadius);
    }
}
