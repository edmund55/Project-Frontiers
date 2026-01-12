using UnityEngine;

public class FlexibleSoundPlayer : MonoBehaviour
{
    public enum PlaybackMode
    {
        Sequential,
        Random
    }

    [Header("Audio Clips")]
    public AudioClip[] clips;

    [Header("Playback Mode")]
    public PlaybackMode playbackMode = PlaybackMode.Random;

    public static int currentIndex = 0;

    public void Play()
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned to CollectableSoundPlayer.");
            return;
        }

        AudioClip clipToPlay;
        if (playbackMode == PlaybackMode.Sequential)
        {
            clipToPlay = clips[currentIndex];
            currentIndex = (currentIndex + 1) % clips.Length;
        }
        else // Random
        {
            clipToPlay = clips[Random.Range(0, clips.Length)];
        }
        
        AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
    }
}
