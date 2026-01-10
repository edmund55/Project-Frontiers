using UnityEngine;

public class PlatformDestroyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        Destroy(transform.parent.gameObject, 2f);
    }
}
