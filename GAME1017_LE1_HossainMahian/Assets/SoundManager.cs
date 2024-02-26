using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    // Reference to UI sliders
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider masterVolumeSlider;
    public Slider stereoPanningSlider;

    // Variables to store current volumes
    private float sfxVolume;
    private float musicVolume;
    private float masterVolume;
    private float stereoPanning;
    public TextMeshProUGUI debugText;

    // Initialization
    private void Start()
    {
        // Set initial values and connect sliders to methods
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        stereoPanningSlider.onValueChanged.AddListener(SetStereoPanning);

        // Set initial volumes
        sfxVolume = sfxVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        masterVolume = masterVolumeSlider.value;
        stereoPanning = stereoPanningSlider.value;
    }

    // Method to set SFX volume
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume * masterVolume;
        Debug.Log("SFX Volume: " + sfxVolume);
        UpdateDebugText();
    }

    // Method to set music volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume * masterVolume;
        Debug.Log("Music Volume: " + musicVolume);
        UpdateDebugText();
    }

    // Method to set master volume
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
       
        SetSFXVolume(sfxVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        Debug.Log("Master Volume: " + masterVolume);
        UpdateDebugText();
    }

    // Method to set stereo panning
    public void SetStereoPanning(float panning)
    {
        stereoPanning = panning;
        Debug.Log("Stereo Panning: " + stereoPanning);
        UpdateDebugText();
    }

    private void UpdateDebugText()
    {
        debugText.text = "SFX Volume: " + sfxVolume + "\n" +
                         "Music Volume: " + musicVolume + "\n" +
                         "Master Volume: " + masterVolume + "\n" +
                         "Stereo Panning: " + stereoPanning;
    }



}
