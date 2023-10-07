using UnityEngine;
using UnityEngine.UI;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField]
    private Slider _masterSlider, _musicSlider, _sfxSlider;

    [SerializeField]
    private SettingsMenu _settingsMenu;

    private void Awake()
    {
        float masterVolume = PlayerPrefs.GetFloat("masterVolume", 1);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1);
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
        _settingsMenu.SetMasterVolume(masterVolume);
        _settingsMenu.SetMusicVolume(masterVolume);
        _settingsMenu.SetSFXVolume(masterVolume);
        _masterSlider.value = masterVolume;
        _musicSlider.value = musicVolume;
        _sfxSlider.value = sfxVolume;
    }
}
