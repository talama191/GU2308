using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Dissappear());
    }

    IEnumerator Dissappear()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void Shoot(Vector3 direction)
    {
        Debug.Log(direction.magnitude);
        rb.AddForce(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
