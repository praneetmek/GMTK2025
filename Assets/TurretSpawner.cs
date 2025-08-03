using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public float minRadius = 5;
    public float maxRadius = 30;
    public Turret turretPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.Turtles == 5)
        {
            GameManager.Instance.Turtles = 0;
            Vector2 spawnPoint = RandomSpawnPoint();
            Instantiate(turretPrefab, transform.position + new Vector3(spawnPoint.x, 0.53f, spawnPoint.y), Quaternion.identity, transform);
        }
    }

    Vector2 RandomSpawnPoint()
    {
        float random = Random.Range(0f, 260f);
        float randomRadius = Random.Range(minRadius, maxRadius);

        return randomRadius * new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    // Gizmo Drawing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        DrawWireCircle(transform.position, minRadius);

        Gizmos.color = Color.green;
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
