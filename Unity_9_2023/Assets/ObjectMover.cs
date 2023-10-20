using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Start()
    {

    }

    public float speed;
    private void FixedUpdate()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
