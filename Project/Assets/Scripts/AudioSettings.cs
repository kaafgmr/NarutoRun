using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SoundFXSlider;

    private void Awake()
    {
        LoadAudioSettings();
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("SoundFXVolume", SoundFXSlider.value);

        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SoundFXSlider.value = PlayerPrefs.GetFloat("SoundFXVolume");
    }

    public void SetAudioTo(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    public float getValueOf(string name)
    {
        return PlayerPrefs.GetFloat(name);
    }
}
