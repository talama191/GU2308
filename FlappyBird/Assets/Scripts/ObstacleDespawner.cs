using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObstacleSpawner.Instance.DespawnObject(collision.gameObject);
    }
}
