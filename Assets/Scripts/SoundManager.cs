using UnityEngine;
using UnityEngine.Audio; 

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Settings")]
    public AudioMixerGroup sfxMixerGroup;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlaySoundAt(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        GameObject tempAudioObj = new GameObject("TempSFX_" + clip.name);
        tempAudioObj.transform.position = position;

        AudioSource source = tempAudioObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.outputAudioMixerGroup = sfxMixerGroup; 
        source.spatialBlend = 0f; 
        
        source.Play();
        Destroy(tempAudioObj, clip.length); 
    }
}