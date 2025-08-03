using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        // Convert the slider value (0-1) to a logarithmic scale for the mixer
        float volume = musicSlider.value;
        float db = Mathf.Log10(volume) * 20; // Convert to decibels
        myMixer.SetFloat("MusicVolume", db);
    }

    public void SetSFXVolume()
    {
        // Convert the slider value (0-1) to a logarithmic scale for the mixer
        float volume = sfxSlider.value;
        float db = Mathf.Log10(volume) * 20; // Convert to decibels
        myMixer.SetFloat("SFXVolume", db);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
