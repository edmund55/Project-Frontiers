using UnityEngine;

public class CollectableLightning : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject triggerEffect;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PowerUpManager.Instance.ActivateDoubleScore(player);
        // AudioSource.PlayClipAtPoint(audioClip, transform.position);
        SoundManager.Instance.PlaySoundAt(audioClip, transform.position);
        Instantiate(triggerEffect, player.transform);
        Destroy(gameObject);
    }
}
