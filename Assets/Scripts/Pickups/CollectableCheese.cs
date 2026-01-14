using UnityEngine;

public class CollectableCheese : MonoBehaviour
{
    public AudioClip audioClip;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PowerUpManager.Instance.ActivateShield(player);
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        Destroy(gameObject);
    }
}
