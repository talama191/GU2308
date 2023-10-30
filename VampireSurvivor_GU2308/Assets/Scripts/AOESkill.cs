using System.Collections.Generic;
using UnityEngine;


public class AOESkill : SkillBase
{
    [SerializeField] private ExplodeEffect explodeEffect;
    public AoeSkillInfo aoeSkillInfo => skillInfo as AoeSkillInfo;

    public override void CastSkill(Vector2 direction)
    {
        rb.velocity = direction * skillInfo.ProjectileSpeed;
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            RaycastHit2D[] enemyHits
             = Physics2D.CircleCastAll(transform.position,
               aoeSkillInfo.AoeRange, Vector2.zero, 0f, layerMask: 1 << 8);
            foreach (var enemy in enemyHits)
            {
                var enemyBase = enemy.collider.gameObject.GetComponent<EnemyBase>();
                enemyBase.TakeDamage(skillInfo.Damage);
            }
            Instantiate(explodeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}