using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected LayerMask enemyLayer;
    protected float damage = 0;
    protected float existTimer;
    protected bool isExplosive;
    protected float radius;

    private void Update()
    {
        existTimer += Time.deltaTime;
        if (existTimer > 15)
        {
            DespawnProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnImpact(other);
        DespawnProjectile();
    }

    private void DespawnProjectile()
    {
        gameObject.SetActive(false);
    }

    public void ShootProjectile(Vector3 dir, float speed, float damage, float gravity, bool isExplosive = false, float radius = 0)
    {
        existTimer = 0;
        rb.velocity = dir * speed + gravity * Vector3.up;
        this.damage = damage;
        this.isExplosive = isExplosive;
        this.radius = radius;
    }

    protected abstract void OnImpact(Collider other);
}
