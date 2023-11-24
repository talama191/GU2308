using UnityEngine;

public class ExplosiveProjectile : ProjectileBase
{
    [SerializeField] float impactForce;
    protected override void OnImpact(Collider other)
    {
        //var damageable = other.GetComponent<Damageable>();
        //if (damageable != null) damageable.TakeDamage(damage);
        if (!isExplosive)
        {
            var damageable = other.GetComponent<Damageable>();
            if (damageable != null) damageable.TakeDamage(damage);
        }
        else
        {
            var contactPoint = other.ClosestPoint(transform.position);
            Debug.Log("Explode");
            EffectManager.Instance.SpawnEffect(radius, contactPoint);
            RaycastHit[] hits = Physics.SphereCastAll(contactPoint, radius, Vector3.right, 1000f, enemyLayer);
            Debug.Log("Raycasthit count" + hits.Length);
            foreach (RaycastHit hit in hits)
            {
                var damageable = hit.rigidbody.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    var flyDirection = (hit.collider.transform.position - contactPoint).normalized;
                    hit.rigidbody.AddForce(flyDirection * impactForce, ForceMode.Impulse);
                }
            }

        }
    }
}