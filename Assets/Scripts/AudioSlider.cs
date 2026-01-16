using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string volumeParameterName;

    public void SetVolume(float volume)
    {
        mixer.SetFloat(volumeParameterName, Mathf.Log10(volume + 0.0001f) * 20);
        Debug.Log(Mathf.Log10(volume + 0.0001f) * 20); 
    }
}