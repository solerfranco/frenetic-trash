using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    [Header("Collision Params")]
    //Este param indica cuanto tiempo en segundos te da de vida agarrar una basura
    [SerializeField]
    [Tooltip("Indicates how much health (in seconds) the trash collected will restore")]
    private float _pickUpAmount;


    //Stun time indica cuanto tiempo estas stuneado
    //Stun cooldown indica el tiempo de invulneravilidad al stun
    [SerializeField]
    private float _stunTime, _stunCooldown;


    [Space]
    [Header("Set up")]
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private SpriteRenderer _shieldRenderer;

    [SerializeField]
    private AudioSource _stunSFX, _pickUpSFX, _powerUpSFX;
    
    private bool _canBeStunned = true;
    private bool _hasShield = false;


    private BoatHealth _health;
    private BoatPowerUps _powerUps;
    private BoatMovement _movement;

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
        if (_hasShield)
        {
            DisableShield();
            yield break;
        }
        if (!_canBeStunned) yield break;
        _stunSFX.Play();
        _movement.enabled = false;
        _renderer.color = Color.red;
        yield return new WaitForSeconds(_stunTime);
        _renderer.color = Color.white;
        _movement.enabled = true;
        StartCoroutine(StunCooldown());
    }

    private void EnableShield()
    {
        _hasShield = true;
        _shieldRenderer.enabled = true;
    }

    private void DisableShield()
    {
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
