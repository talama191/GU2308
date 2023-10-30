using UnityEngine;

public class EnemyBasicSkill : SkillBase
{
    public override void CastSkill(Vector2 direction)
    {
        rb.velocity = direction * skillInfo.ProjectileSpeed;
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Instance.TakeDamage(skillInfo.Damage);
            Destroy(gameObject);
        }
    }
}
