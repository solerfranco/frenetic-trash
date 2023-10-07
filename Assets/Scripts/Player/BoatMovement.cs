using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement Params")]
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _rotationSpeed;

    [Space]
    [Header("Set Up")]
    [SerializeField]
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    [HideInInspector]
    public PlayerInputActions _inputActions;
    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void FixedUpdate()
    {
        _movementInput = _inputActions.Player.Movement.ReadValue<Vector2>();
        _rb.AddForce(transform.up * _acceleration * _movementInput.y * Time.fixedDeltaTime, ForceMode2D.Force);
        _rb.AddTorque(-_movementInput.x * _rotationSpeed * Time.fixedDeltaTime);

        Vector3 velocity = _rb.velocity;
        _rb.velocity = Vector3.zero;
        _rb.velocity = transform.up * velocity.magnitude;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }
}
