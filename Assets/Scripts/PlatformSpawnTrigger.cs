using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawnTrigger : MonoBehaviour
{
    private bool triggered = false; // avoid spawning twice, since player have two collider rn
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        FindFirstObjectByType<PlatformSpawner>().SpawnNextPath();

        triggered = true;
    }
}

