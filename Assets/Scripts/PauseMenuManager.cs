using UnityEngine;
using UnityEngine.Audio;
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

    private void Start()
    {
        menuHolder.SetActive(false);
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
    }
}
