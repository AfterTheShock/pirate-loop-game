using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    private bool isInMenu = false;

    [SerializeField] GameObject menuHolder;

    [SerializeField] AudioMixer masterMixer;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider ambianceSlider;
    [SerializeField] Slider masterSlider;

    [SerializeField] GameObject mainMenuToTurnOn;

    [SerializeField] TMP_Dropdown languageDropdown;

    private void Start()
    {
        menuHolder.SetActive(false);

        masterMixer.GetFloat("MasterVolume", out float masterVolume);
        masterMixer.GetFloat("MusicVolume", out float musicVolume);
        masterMixer.GetFloat("SFXVolume", out float sfxVolume);
        masterMixer.GetFloat("AmbianceVolume", out float ambianceVolume);

        masterSlider.value = Mathf.Pow(10, masterVolume / 20);
        musicSlider.value = Mathf.Pow(10, musicVolume / 20);
        SFXSlider.value = Mathf.Pow(10, sfxVolume / 20);
        ambianceSlider.value = Mathf.Pow(10, ambianceVolume / 20);

        InicializeLanguageDropdown();
    }

    private void Update()
    {


        if (!isInMenu && Time.timeScale != 0 && Input.GetKeyDown(KeyCode.Escape)) EnterMenu();
        else
        if (isInMenu && Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Escape)) ExitMenu();
    }

    public void OnChangeMusicSlider()
    {

        float volume = Mathf.Log10(musicSlider.value) * 20; // Convert from dB to linear for slider

        masterMixer.SetFloat("MusicVolume", volume);

    }

    public void OnChangeSFXSlider()
    {

        float volume = Mathf.Log10(SFXSlider.value) * 20; // Convert from dB to linear for slider

        masterMixer.SetFloat("SFXVolume", volume);

    }

    public void OnChangeAmbianceSlider()
    {

        float volume = Mathf.Log10(ambianceSlider.value) * 20; // Convert from dB to linear for slider

        masterMixer.SetFloat("AmbianceVolume", volume);

    }

    public void OnChangeMasterSlider()
    {

        float volume = Mathf.Log10(masterSlider.value) * 20; // Convert from dB to linear for slider

        masterMixer.SetFloat("MasterVolume", volume);

    }

    private void EnterMenu()
    {
        Time.timeScale = 0;
        isInMenu = true;
        menuHolder.SetActive(true);

    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        isInMenu = false;
        menuHolder.SetActive(false);

        if (mainMenuToTurnOn != null) mainMenuToTurnOn.SetActive(true);
    }

    public void InicializeLanguageDropdown()
    {
        languageDropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        languageDropdown.value = 1;
    }

    public void ChangeLanguageFromDropdown(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
