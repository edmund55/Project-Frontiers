using UnityEngine;

public class PlatformSpawnTrigger : MonoBehaviour
{
    [Header("Powerup Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.75f;

    public GameObject[] powerupPrefabs; 

    public float powerupZ = -0.48f;
    public float[] laneXPositions = { -0.33f, 0f, 0.33f };

    private Vector3 localScale = new Vector3(0.8333f, 0, 0.0166f);

    private bool triggered = false; // avoid spawning twice, since player have two collider rn
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        PlatformSpawner spawner = FindFirstObjectByType<PlatformSpawner>();

        GameObject nextPlatform = spawner.SpawnNextPath();

        TrySpawnPowerup(nextPlatform);
    }

    void TrySpawnPowerup(GameObject platform)
    {
        if (Random.value > spawnChance) return;
        if (powerupPrefabs.Length == 0) return;

        GameObject prefab = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];

        float x = laneXPositions[Random.Range(0, laneXPositions.Length)];

        // Spawn as child of NEXT platform
        GameObject powerup = Instantiate(prefab, platform.transform);

        powerup.transform.localPosition = new Vector3(
            x,
            powerup.transform.localPosition.y,
            powerupZ
        );

        SetWorldScale(powerup.transform, Vector3.one);
    }

    void SetWorldScale(Transform target, Vector3 desiredWorldScale)
    {
        if (target.parent == null)
        {
            target.localScale = desiredWorldScale;
            return;
        }

        Vector3 parentScale = target.parent.lossyScale;

        target.localScale = new Vector3(
            desiredWorldScale.x / parentScale.x,
            desiredWorldScale.y / parentScale.y,
            desiredWorldScale.z / parentScale.z
        );
    }
}

