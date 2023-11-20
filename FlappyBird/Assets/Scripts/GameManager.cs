using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject cameraMain;
    [SerializeField] private FlappyController flappy;
    [SerializeField] private GameObject ground1;
    [SerializeField] private GameObject ground2;

    private Vector3 cameraStartPos;
    private Vector3 flappyStartPos;
    private Vector3 ground1StartPos;
    private Vector3 ground2StartPos;

    private void Awake()
    {
        cameraStartPos = cameraMain.transform.position;
        flappyStartPos = flappy.transform.position;
        ground1StartPos = ground1.transform.position;
        ground2StartPos = ground2.transform.position;
    }

    public void Reset()
    {
        cameraMain.transform.position = cameraStartPos;
        flappy.transform.position = flappyStartPos;
        ground1.transform.position = ground1StartPos;
        ground2.transform.position = ground2StartPos;
        ObstacleSpawner.Instance.flappyTravelCounter = 0;
        flappy.ResetGame();
    }
}
