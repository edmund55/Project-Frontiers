using UnityEngine;

public class CollectableCheese : MonoBehaviour
{

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PowerUpManager.Instance.ActivateShield(player);
        Destroy(gameObject);
    }
}
