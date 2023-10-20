using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    public static TestCoroutine Instance { get; private set; }
    private float hp;
    public event Action<float> UpdatePlayerHP;
    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = 100;
        // SceneLoader.Instance.LoadRandomScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(float damage)
    {
        hp -= damage;
        UpdatePlayerHP?.Invoke(hp);
    }

}
