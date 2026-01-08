using UnityEngine;

public class PlatformDestroyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Destroy(transform.parent.gameObject, 2f);
    }
}
