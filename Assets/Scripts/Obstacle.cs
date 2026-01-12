using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public AudioClip bulletCrashSound;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        bool playerKilled = player.TakeDamage(1);

        if (playerKilled)
        {
            player.DisableControl();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBulletCrashSound()
    {
        AudioSource.PlayClipAtPoint(bulletCrashSound, transform.position);
    }
}
