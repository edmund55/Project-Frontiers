using UnityEngine;

public class CollectableBattery : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject triggerEffect;

    [SerializeField] private float RechargeAmount = 30f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        player.RechargeBattery(RechargeAmount);
        // AudioSource.PlayClipAtPoint(audioClip, transform.position);
        SoundManager.Instance.PlaySoundAt(audioClip, transform.position);
        Instantiate(triggerEffect, player.transform);
        Destroy(gameObject);
    }
}
