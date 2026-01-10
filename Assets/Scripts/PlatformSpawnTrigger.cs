using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawnTrigger : MonoBehaviour
{
    public List<GameObject> pathPrefabs;
    public float pathLength = 60f;

    private static Vector3 nextSpawnPosition;
    private static bool initialized = false;
    private bool triggered = false;

    private void Start()
    {
        if (!initialized)
        {
            nextSpawnPosition = transform.parent.position;
            initialized = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        SpawnNextPath();
    }


    void SpawnNextPath()
    {
        int index = Random.Range(0, pathPrefabs.Count);

        nextSpawnPosition += Vector3.forward * pathLength;

        Instantiate(
            pathPrefabs[index],
            nextSpawnPosition,
            Quaternion.identity
        );
    }

}

