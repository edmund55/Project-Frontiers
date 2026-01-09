using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();

            if (player != null)
            {
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
        }
    }
}
