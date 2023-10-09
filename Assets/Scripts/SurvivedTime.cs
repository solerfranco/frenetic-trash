using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivedTime : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _survivedTimeTMP, _counterTMP;

    void Start()
    {
        _survivedTimeTMP.text = "You survived " + _counterTMP.text + "!";
    }
}
