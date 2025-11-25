using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //TO DO Zenject
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;

    private void Awake()
    {
        // === ВОССТАНОВЛЕНИЕ НАСТРОЕК ===
        
        // 1. Восстанавливаем громкость
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // 2. Получаем список разрешений
        resolutions = Screen.resolutions.Select(r => new Resolution { width = r.width, height = r.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        var options = resolutions.Select(res => res.width + " x " + res.height).ToList();

        resolutionDropdown.AddOptions(options);

        // Восстанавливаем разрешение
        int savedResIndex = PlayerPrefs.GetInt("ResolutionIndex", Screen.resolutions.Length - 1);
        resolutionDropdown.value = savedResIndex;
        currentResolutionIndex = savedResIndex;

        resolutionDropdown.RefreshShownValue();
        SetResolution(savedResIndex);

        // 3. Восстанавливаем fullscreen
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = savedFullscreen;
        SetFullscreen(savedFullscreen);
    }

    // === УСТАНОВКА ГРОМКОСТИ ===
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // логарифмика
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    // === УСТАНОВКА РАЗРЕШЕНИЯ ===
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    // === УСТАНОВКА FULLSCREEN ===
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}
