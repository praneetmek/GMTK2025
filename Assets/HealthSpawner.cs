using UnityEngine;
using System.Collections;

public class HealthSpawner : MonoBehaviour
{
    public float minRadius = 5f;
    public float maxRadius = 30f;
    public GameObject healthPickupPrefab; // Assign your Health Pickup prefab here
    public float spawnHeight = 5f; // Vertical offset for pickup height
    public float spawnInterval = 60f; // Time in seconds between spawns

    private void Start()
    {
        // Spawn initial heart
        SpawnHealthPickup();

        // Start timed spawning loop
        StartCoroutine(SpawnHealthOverTime());
    }

    private IEnumerator SpawnHealthOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnHealthPickup();
        }
    }

    public void SpawnHealthPickup()
    {
        Vector2 spawnPoint = RandomSpawnPoint();
        Vector3 spawnPosition = transform.position + new Vector3(spawnPoint.x, spawnHeight, spawnPoint.y);

        Instantiate(healthPickupPrefab, spawnPosition, Quaternion.identity, transform);
    }

    Vector2 RandomSpawnPoint()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(minRadius, maxRadius);

        return randomRadius * new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
    }

    // Gizmo Drawing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        DrawWireCircle(transform.position, minRadius);

        Gizmos.color = Color.yellow;
        DrawWireCircle(transform.position, maxRadius);
    }

    void DrawWireCircle(Vector3 center, float radius)
    {
        int segments = 64;
        float angle = 0f;

        Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        for (int i = 0; i <= segments; i++)
        {
            angle += 2 * Mathf.PI / segments;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }
}
