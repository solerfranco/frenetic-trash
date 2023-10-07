using UnityEngine;
using TMPro;
using System.Linq;

public class ResolutionSettings : MonoBehaviour
{
    private Resolution[] resolutions;

    [SerializeField]
    private GameObject _previousButton, _nextButton;
    
    [SerializeField]
    private TextMeshProUGUI _textValue;

    private int _index;
    private int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
            _previousButton.SetActive(_index > 0);
            _nextButton.SetActive(_index < resolutions.Length - 1);
            ChangeResolution();
            _textValue.text = resolutions[_index].width.ToString() + " x " + resolutions[_index].height.ToString();
        }
    }

    private void ChangeResolution()
    {
        Resolution resolution = resolutions[Index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void IncrementIndex() { Index++; }

    public void DecrementIndex() { Index--; }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
            return;
        }

        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRateRatio.value == 60).ToArray();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                Index = i;
            }
        }
    }
}
