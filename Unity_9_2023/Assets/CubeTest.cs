using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float Speed;
    [SerializeField] private float timeScaleMultiplier;
    [SerializeField] private Transform otherTransform;
    [HideInInspector] public int[] array;
    [Header("Config")]
    public Boolean isTrue;
    public Vehicle vehicle;

    private void Start()
    {
        Time.timeScale = timeScaleMultiplier;
        // transform.position;
        // transform.localPosition;
        // Time.timeScale = 0.5f;

    }

    private void Update()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");
        transform.position += Vector3.up * yAxis * Time.deltaTime * Speed + Vector3.right * xAxis * Time.deltaTime * Speed;

    }

    private void FixedUpdate()
    {
        // otherTransform.Translate(Vector3.up * Time.deltaTime * Speed);
    }
}

[Serializable]
public class Vehicle
{
    public float speed;
}
