using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public float minRadius = 5;
    public float maxRadius = 30;
    public Turret turretPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
}
