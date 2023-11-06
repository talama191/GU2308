using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var flappy = col.gameObject.GetComponent<FlappyController>();
            flappy.TriggerGameOver();
        }
    }
}
