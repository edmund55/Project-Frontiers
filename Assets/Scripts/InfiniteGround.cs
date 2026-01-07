using UnityEngine;
using System.Collections.Generic;

public class InfiniteGround : MonoBehaviour
{
    public List<GameObject> pathPrefabs;
    public float pathLength = 30f;

    private static Vector3 nextSpawnPosition;
    private static bool initialized = false;

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
        if (!other.CompareTag("Player")) return;

        SpawnNextPath();
        Destroy(transform.parent.gameObject, 5f);


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
}

