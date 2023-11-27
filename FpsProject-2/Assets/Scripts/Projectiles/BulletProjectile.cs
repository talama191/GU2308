using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : ProjectileBase
{
    protected override void OnImpact(Collider other)
    {
        var damageable = other.GetComponent<Damageable>();
        if (damageable != null) damageable.TakeDamage(damage);
    }
}
