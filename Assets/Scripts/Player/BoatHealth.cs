using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatHealth : MonoBehaviour
{
    [Header("Health Params")]
    public float _maxHealth;

    [Space]
    [Header("Set Up")]
    [SerializeField]
    private AudioSource _deathSFX;
    
    private BoatCollision _boatCollision;
    private BoatMovement _boatMovement;

    private float _currentHealth;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
        }
    }

    [SerializeField]
    private Image _healthBar;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _boatCollision = GetComponent<BoatCollision>();
        _boatMovement = GetComponent<BoatMovement>();
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            _boatCollision.enabled = false;
            _boatMovement.enabled = false;
            enabled = false;
            Destroy(gameObject, 1);
            _deathSFX.Play();
        }
        else
        {
            CurrentHealth -= Time.deltaTime;
            _healthBar.fillAmount = CurrentHealth / _maxHealth;
        }
    }
}
