using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string volumeParameterName;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        
        if (mixer.GetFloat(volumeParameterName, out float currentDB))
        {
            float linearValue = Mathf.Pow(10, currentDB / 20.0f) - 0.0001f;

            _slider.SetValueWithoutNotify(linearValue);
        }
    }
    
    public void SetVolume(float volume)
    {
        mixer.SetFloat(volumeParameterName, Mathf.Log10(volume + 0.0001f) * 20);
    }
}