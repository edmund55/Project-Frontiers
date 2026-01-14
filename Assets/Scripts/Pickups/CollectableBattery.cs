using UnityEngine;

public class CollectableBattery : MonoBehaviour
{
    [SerializeField] private float RechargeAmount = 30f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        player.RechargeBattery(RechargeAmount);

        Destroy(gameObject);
    }
}
