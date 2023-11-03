using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private List<EnemyBase> enemyPrefabs;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float spawnInterval;
    private float spawnTimer;
    private List<EnemyBase> enemies = new List<EnemyBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public EnemyBase GetNearestEnemy(Vector3 from)
    {
        if (enemies.Count == 0) return null;
        EnemyBase nearestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (var enemy in enemies)
        {
            var sqrDistance = (from - enemy.transform.position).sqrMagnitude;
            if (minDistance > sqrDistance)
            {
                minDistance = sqrDistance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemy(enemyPrefabs.OrderBy(e => Random.Range(0f, 1f)).FirstOrDefault());
        }
    }

    private void SpawnEnemy(EnemyBase enemy)
    {
        var playerPosition = PlayerController.Instance.transform.position;
        var degree = Random.Range(0f, 360f);
        var randomVector = Quaternion.Euler(0, 0, degree) * Vector2.up;
        var distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        var clampedVector = randomVector.normalized * distance;

        var spawnPos = playerPosition + clampedVector;
        var enemyBase = Instantiate(enemy, spawnPos, Quaternion.identity);
        enemies.Add(enemyBase);
    }

    public void RemoveEnemyFromList(EnemyBase enemy)
    {
        enemies.Remove(enemy);
    }
}
