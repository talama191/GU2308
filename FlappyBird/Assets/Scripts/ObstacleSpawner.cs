using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [SerializeField] private float upBound;
    [SerializeField] private float bottomBound;
    [SerializeField] private float spawnDistance;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<GameObject> obstaclePools;

    public float flappyTravelCounter;

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
            //var obstacle = Instantiate(obstaclePrefab, new Vector2(flappyX + 8, y), Quaternion.identity, obstacleContainer);
            var obstacle = GetPooledObject();
            obstacle.transform.position = new Vector2(flappyX + 8, y);

            flappyTravelCounter -= spawnDistance;
        }
    }

    private GameObject GetPooledObject()
    {
        var obstacle = obstaclePools.FirstOrDefault(o => !o.activeInHierarchy);
        obstacle.SetActive(true);
        return obstacle;
    }

    public void DespawnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

}
