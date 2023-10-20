using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float MaxHp;
    private float currentHp;

    private void Awake()
    {
        currentHp = MaxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0) Destroy(gameObject);
    }
}
