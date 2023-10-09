using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalCounter : MonoBehaviour
{
    [HideInInspector]
    public float CurrentTime = 0;

    [SerializeField]
    private BoatHealth _health;

    [SerializeField]
    private TextMeshProUGUI _counterTMP;
    private void Update()
    {
        CurrentTime += Time.deltaTime;
        _counterTMP.text = TimeSpan.FromSeconds(CurrentTime).Minutes.ToString("00") + ":" + TimeSpan.FromSeconds(CurrentTime).Seconds.ToString("00");
    }
}
