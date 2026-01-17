using UnityEngine;

public class CollectableGear : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject triggerEffect;

    [SerializeField] private int lifeAmount = 1;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PowerUpManager.Instance.GiveLife(player, lifeAmount);
        // AudioSource.PlayClipAtPoint(audioClip, transform.position);
        SoundManager.Instance.PlaySoundAt(audioClip, transform.position);
        Instantiate(triggerEffect, player.transform);
        Destroy(gameObject);
    }
}
