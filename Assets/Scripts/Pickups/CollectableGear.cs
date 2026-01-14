using TMPro;
using UnityEngine;

public class CollectableGear : MonoBehaviour
{
    [SerializeField] private int lifeAmount = 1;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PowerUpManager.Instance.GiveLife(player, lifeAmount);
        Destroy(gameObject);
    }
}
