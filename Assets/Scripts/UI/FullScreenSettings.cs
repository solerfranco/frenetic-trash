using UnityEngine;
using TMPro;
using System;

public class FullScreenSettings : MonoBehaviour
{
    private class FullScreenType
    {
        public FullScreenMode mode;
        public string name;

        public FullScreenType(FullScreenMode mode, string name)
        {
            this.mode = mode;
            this.name = name;
        }
    }

    private readonly FullScreenType[] _types =
    {
        new FullScreenType(FullScreenMode.ExclusiveFullScreen, "FullScreen"),
        new FullScreenType(FullScreenMode.FullScreenWindow, "Borderless"),
        new FullScreenType(FullScreenMode.Windowed, "Windowed")
    };

    [SerializeField]
    private GameObject _previousButton, _nextButton;

    [SerializeField]
    private TextMeshProUGUI _textValue;

    private int _index;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
            _previousButton.SetActive(_index > 0);
            _nextButton.SetActive(_index < _types.Length - 1);
            ChangeFullScreenMode();
            _textValue.text = _types[_index].name;
        }
    }

    private void ChangeFullScreenMode()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, _types[Index].mode);
    }

    public void IncrementIndex() { Index++; }

    public void DecrementIndex() { Index--; }

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
            return;
        }
        Index = Array.FindIndex(_types, type => type.mode == Screen.fullScreenMode);
    }
}
