using UnityEngine;

public class CollectableLightning : MonoBehaviour
{
    public AudioClip audioClip;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PowerUpManager.Instance.ActivateDoubleScore();
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        Destroy(gameObject);
    }
}
