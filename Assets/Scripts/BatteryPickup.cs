using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] private float RechargeAmount = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            player.RechargeBattery(RechargeAmount);
        }

        gameObject.SetActive(false);
    }
}
