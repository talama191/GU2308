using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkill : SkillBase
{
    public override void CastSkill(Vector2 direction)
    {
        rb.velocity = direction * skillInfo.ProjectileSpeed;
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.GetComponent<EnemyBase>();
            enemy.TakeDamage(skillInfo.Damage);
            Destroy(gameObject);
        }
    }

}
