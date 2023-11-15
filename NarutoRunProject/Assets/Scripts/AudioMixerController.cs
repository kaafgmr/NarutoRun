using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string AudioName;

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(AudioName, ToDecibels(value));
    }

    private float ToDecibels(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}