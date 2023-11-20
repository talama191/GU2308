using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float damage = 0;
    private float existTimer;

    private void Update()
    {
        existTimer += Time.deltaTime;
        if (existTimer > 2)
        {
            DespawnProjectile();
        }
    }

    public void ShootProjectile(Vector3 dir, float speed, float damage, float gravity)
    {
        existTimer = 0;
        rb.velocity = dir * speed + gravity * Vector3.up;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damageable")
        {
            var damageable = other.GetComponent<Damageable>();
            if (damageable != null) damageable.TakeDamage(damage);
        }
        DespawnProjectile();
    }

    private void DespawnProjectile()
    {
        gameObject.SetActive(false);
    }
}
