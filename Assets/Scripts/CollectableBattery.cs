using UnityEngine;

public class CollectableBattery : MonoBehaviour
{
    [SerializeField] private float RechargeAmount = 30f;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        player.RechargeBattery(RechargeAmount);

        Destroy(gameObject);
    }
}
