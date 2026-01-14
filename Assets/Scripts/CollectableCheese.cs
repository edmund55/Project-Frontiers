using UnityEngine;

public class CollectableCheese : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        PowerUpManager.Instance.ActivateShield(player);
        Destroy(gameObject);
    }
}
