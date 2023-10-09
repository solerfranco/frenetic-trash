using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private int currentSlide = 0;
    public int levelIndex = 3;

    [SerializeField]
    private GameObject _firstSlide, _secondSlide, _thirdSlide;

    public void OnChangeSlide()
    {
        currentSlide++;
        switch (currentSlide)
        {
            case 0:
                _firstSlide.SetActive(true);
                break;
            case 1:
                _secondSlide.SetActive(true);
                break;
            case 2:
                _thirdSlide.SetActive(true);
                break;
            default:
                SceneManager.LoadScene(levelIndex);
                break;
        }
    }
}
