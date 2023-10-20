using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    private PlayerController pc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void ShootBullet(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
        StartCoroutine(SelfDestroy());
    }

    // private void OnTriggerEnter(Collider other)
    // {

    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Deal damage to enemy
            Debug.Log("hit");
            var enemy = other.GetComponent<EnemyBase>();
            if (pc == null) pc = PlayerController.Instance;
            enemy.TakeDamage(pc.skill1Damage);
            Destroy(gameObject);
        }
    }
}
