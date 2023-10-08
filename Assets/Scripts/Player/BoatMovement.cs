using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement Params")]
    public float Acceleration;
    public float Torque;

    [Space]
    [Header("Set Up")]
    public Rigidbody2D Rb;
    private Vector2 _movementInput;

    [SerializeField]
    private SpriteRenderer _playerRenderer;

    [SerializeField]
    private Animator _playerAnim;

    [HideInInspector]
    public PlayerInputActions InputActions;
    private void Awake()
    {
        InputActions = new PlayerInputActions();
    }

    private void FixedUpdate()
    {
        _movementInput = InputActions.Player.Movement.ReadValue<Vector2>();
        Rb.AddForce(transform.up * Acceleration * _movementInput.y * Time.fixedDeltaTime, ForceMode2D.Force);
        Rb.AddTorque(-_movementInput.x * Torque * Time.fixedDeltaTime);
        _playerAnim.SetFloat("Velocity", Rb.velocity.sqrMagnitude);
        _playerAnim.SetFloat("Horizontal", transform.up.x);
        _playerAnim.SetFloat("Vertical", transform.up.y);

        //Vector3 velocity = Rb.velocity;
        //Rb.velocity = Vector3.zero;
        //Rb.velocity = transform.up * velocity.magnitude;
    }

    private void Update()
    {
        _playerRenderer.transform.position = transform.position;
    }

    private void OnEnable()
    {
        InputActions.Player.Enable();
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
    }
}
