using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> pathPrefabs;
    public float pathLength = 60f;

    private Vector3 nextSpawnPosition;

    void Start()
    {
        nextSpawnPosition = Vector3.zero;
    }

    public void SpawnNextPath()
    {
        int index = Random.Range(0, pathPrefabs.Count);
        nextSpawnPosition += Vector3.forward * pathLength;

        Instantiate(pathPrefabs[index], nextSpawnPosition, Quaternion.identity);
    }
}
