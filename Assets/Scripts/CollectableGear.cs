using UnityEngine;

public class CollectableGear : MonoBehaviour
{
    [SerializeField] private int lifeAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        PowerUpManager.Instance.GiveLife(player, lifeAmount);
        Destroy(gameObject);
    }
}
