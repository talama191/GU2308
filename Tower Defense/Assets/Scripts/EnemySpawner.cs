using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField] private FieldSlot startSlot;
    [SerializeField] private FieldSlot endSlot;
    [SerializeField] private List<WaveConfig> waveConfigs;

    private int currentWaveIndex = 0;
    private float spawnTimer = 0;
    private Wave currentWave;

    public FieldSlot StartSlot => startSlot;
    public FieldSlot EndSlot => endSlot;

    private void Start()
    {
        currentWaveIndex = 0;
        currentWave = new Wave(waveConfigs[currentWaveIndex]);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.Config.SpawnDelay)
        {
            if (currentWave.SpawnEnemy())
            {
                spawnTimer -= currentWave.Config.SpawnDelay;
                var enemy = Instantiate(currentWave.Config.EnemyData.EnemyPrefab);
                enemy.transform.position = StartSlot.transform.position;
                enemy.SetupData(currentWave.Config.EnemyData, StartSlot, EndSlot);
            }
            else
            {
                currentWaveIndex++;
                if (currentWaveIndex < waveConfigs.Count)
                {
                    currentWave = new Wave(waveConfigs[currentWaveIndex]);
                }
            }
        }
    }
}

public class Wave
{
    private WaveConfig config;
    private float spawnedEnemyCount;

    public WaveConfig Config => config;

    public Wave(WaveConfig config)
    {
        this.config = config;
    }

    public bool SpawnEnemy()
    {
        if (spawnedEnemyCount >= config.TotalEnemyCount)
        {
            return false;
        }
        spawnedEnemyCount++;
        return true;
    }
}