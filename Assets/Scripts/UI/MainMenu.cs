using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _quitButton, _lock;

    [SerializeField]
    private Button survivalButton;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            _quitButton.SetActive(false);
        }
        if(PlayerPrefs.GetInt("SurvivalMode", 0) == 1)
        {
            _lock.SetActive(false);
            survivalButton.interactable = true;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void PlaySurvival()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
