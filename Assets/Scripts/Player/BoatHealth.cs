using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private bool _flashing;

    [SerializeField]
    private Color _initialColor, _dangerColor;

    [SerializeField]
    private GameObject _gameOverScreen;

    [SerializeField]
    private GameObject _counter;

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
            _gameOverScreen.SetActive(true);
            StartCoroutine(LoadMainMenu());
            _counter.SetActive(false);
            enabled = false;
            _deathSFX.Play();
        }
        else
        {
            CurrentHealth -= Time.deltaTime;
            _healthBar.fillAmount = CurrentHealth / _maxHealth;
            if (!_flashing && CurrentHealth <= _maxHealth / 2) StartCoroutine(FlashColor());
        }
    }

    private IEnumerator FlashColor()
    {
        _flashing = true;
        _healthBar.color = _initialColor;
        yield return new WaitForSeconds(0.25f);
        _healthBar.color = _dangerColor;
        yield return new WaitForSeconds(0.25f);
        _healthBar.color = _initialColor;
        _flashing = false;
    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
