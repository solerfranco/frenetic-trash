using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private float _initialTime;

    private float _currentTime;

    [SerializeField]
    private GameObject _winScreen;

    [SerializeField]
    private BoatHealth _health;

    [SerializeField]
    private TextMeshProUGUI _counterTMP;
    private void Start()
    {
        _currentTime = _initialTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _counterTMP.text = TimeSpan.FromSeconds(_currentTime).Minutes.ToString("00") + ":" + TimeSpan.FromSeconds(_currentTime).Seconds.ToString("00");
        if(_currentTime <= 0)
        {
            StartCoroutine(WinScreen());
        }
    }

    private IEnumerator WinScreen()
    {
        PlayerPrefs.SetInt("SurvivalMode", 1);
        _health.enabled = false;
        _winScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
