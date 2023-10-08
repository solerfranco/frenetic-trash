using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoatCollision : MonoBehaviour
{
    [Header("Collision Params")]
    //Este param indica cuanto tiempo en segundos te da de vida agarrar una basura
    [SerializeField]
    [Tooltip("Indicates how much health (in seconds) the trash collected will restore")]
    private float _pickUpAmount;

    [Tooltip("How fast will the boat go when bumping an obstacle")]
    [SerializeField]
    private float _bumpSpeed;

    //Stun time indica cuanto tiempo estas stuneado
    //Stun cooldown indica el tiempo de invulneravilidad al stun
    [SerializeField]
    private float _stunTime, _stunCooldown;


    [Space]
    [Header("Set up")]
    [SerializeField]
    private SpriteRenderer _renderer, _trashBagRenderer;

    [SerializeField]
    private Animator _playerAnim;

    [SerializeField]
    private SpriteRenderer _shieldRenderer;

    [SerializeField]
    private AudioSource _stunSFX, _pickUpSFX, _powerUpSFX, _brokenShieldSFX;
    
    private bool _canBeStunned = true;
    private bool _hasShield = false;


    private BoatHealth _health;
    private BoatPowerUps _powerUps;
    private BoatMovement _movement;
    private int _trashCount;
    [SerializeField]
    private TextMeshProUGUI _trashCounterTMP;

    private void Awake()
    {
        _health = GetComponent<BoatHealth>();
        _powerUps = GetComponent<BoatPowerUps>();
        _movement = GetComponent<BoatMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash"))
        {
            _pickUpSFX.Play();
            _health.CurrentHealth += Mathf.Clamp(_pickUpAmount, 0, _health._maxHealth);
            _trashCount++;
            _trashCounterTMP.text = _trashCount.ToString();
            PlayerPrefs.SetInt("TrashCount", _trashCount);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Shield"))
        {
            _powerUpSFX.Play();
            Destroy(collision.gameObject);
            EnableShield();
        }

        if (collision.CompareTag("Magnet"))
        {
            _powerUpSFX.Play();
            Destroy(collision.gameObject);
            _powerUps.EnableMagnet();
        }

        if (collision.CompareTag("Speed"))
        {
            _powerUpSFX.Play();
            Destroy(collision.gameObject);
            _powerUps.EnableSuperSpeed();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(Stun());
        }
    }

    private IEnumerator Stun()
    {
        _movement.Rb.velocity = -_movement.Rb.velocity.normalized * _bumpSpeed;
        if (_hasShield)
        {
            DisableShield();
            yield break;
        }
        if (!_canBeStunned) yield break;
        _stunSFX.Play();
        _playerAnim.SetBool("Stun", true);
        _trashBagRenderer.enabled = true;
        _movement.InputActions.Disable();
        yield return new WaitForSeconds(_stunTime);
        _playerAnim.SetBool("Stun", false);
        _trashBagRenderer.enabled = false;
        _movement.InputActions.Enable();
        StartCoroutine(StunCooldown());
    }

    private void EnableShield()
    {
        _hasShield = true;
        _shieldRenderer.enabled = true;
    }

    private void DisableShield()
    {
        _brokenShieldSFX.Play();
        _shieldRenderer.enabled = false;
        _hasShield = false;
    }

    private IEnumerator StunCooldown()
    {
        _canBeStunned = false;
        yield return new WaitForSeconds(_stunCooldown);
        _canBeStunned = true;
    }
}
