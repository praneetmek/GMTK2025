using UnityEngine;
using static GameManager;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Transform world;
    public GameObject target;

    public float spawnRadius = 30;
    public EmenyController enemy;

    private float _timeSinceLastSpawn;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timeSinceLastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;
        int numEnemies = GetNumberOfEnemiesInWorld();
        if(numEnemies < 10 && _timeSinceLastSpawn > 0.5)
        {
            _timeSinceLastSpawn = 0;
            SpawnEnemy();
        }
    }

    int GetNumberOfEnemiesInWorld()
    {
        int numChildren = 0;
        for (int i = 0; i < world.childCount; i++)
        {
            if (world.GetChild(i).tag == "Enemy")
            {
                numChildren++;
            }
        }
        return numChildren;
    }

    void SpawnEnemy()
    {
        Vector2 randomSpawnPoint = RandomSpawnPoint();
        EmenyController e = Instantiate(enemy, transform.position + new Vector3(randomSpawnPoint.x, 0.55f, randomSpawnPoint.y), Quaternion.identity, world);
        e.Target = target;
    }

    Vector2 RandomSpawnPoint()
    {
        float random = Random.Range(0f, 260f);
        return spawnRadius * new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
}
