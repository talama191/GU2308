using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [SerializeField] private float upBound;
    [SerializeField] private float bottomBound;
    [SerializeField] private float spawnDistance;
    [SerializeField] private Transform obstacleContainer;
    [SerializeField] private GameObject obstaclePrefab;

    private float flappyTravelCounter;

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

    public void UpdateFlappyDistance(float distance, float flappyX)
    {
        flappyTravelCounter += distance;
        if (flappyTravelCounter >= spawnDistance)
        {
            var y = Random.Range(bottomBound, upBound);
            var obstacle = Instantiate(obstaclePrefab, new Vector2(flappyX + 8, y), Quaternion.identity, obstacleContainer);

            flappyTravelCounter -= spawnDistance;
        }
    }

}
