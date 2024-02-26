using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soundmanager : MonoBehaviour
{

    public GameObject menu_button;
    public GameObject Sound_button;

    public AudioSource musicSource;
    public Slider musicSlider;
    public AudioSource sfxSource;
    public Slider sfxSlider;



    
    void Start()
    {
        Sound_button.SetActive(false);
        menu_button.SetActive(true);

         if (musicSlider != null){
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        }

        if (sfxSlider != null){
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        }
    }
    
    public void sound_button(){
      
        Sound_button.SetActive(true);
        menu_button.SetActive(false);

    }

    public void Ok_button(){
      
        Sound_button.SetActive(false);
        menu_button.SetActive(true);

    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }


}
