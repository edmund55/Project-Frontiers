using UnityEngine;

public class CollectableLightning : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        PowerUpManager.Instance.ActivateDoubleScore();
        Destroy(gameObject);
    }
}
