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
    
    private BoatCollision boatCollision;
    private BoatMovement boatMovement;

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
        boatCollision = GetComponent<BoatCollision>();
        boatMovement = GetComponent<BoatMovement>();
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            boatCollision.enabled = false;
            boatMovement.enabled = false;
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
