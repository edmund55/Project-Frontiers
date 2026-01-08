using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();

            if (player != null)
            {
                bool playerKilled = player.TakeDamage(1);

                if (playerKilled)
                {
                    player.DisableControl();
                } else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
