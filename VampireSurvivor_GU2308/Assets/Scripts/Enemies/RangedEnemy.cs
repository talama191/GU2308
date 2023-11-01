using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private float agroRange;
    [SerializeField] private SkillBase bulletPrefab;
    protected override void AIControl(float dt)
    {
        var playerPos = PlayerController.Instance.transform.position;
        var moveDirection = playerPos - transform.position;

        var distance = moveDirection.magnitude;
        if (distance <= agroRange)
        {
            //Trang thai gan nguoi choi
            if (attackTimer >= attackCooldown)
            {
                attackTimer = attackCooldown - attackTimer;
                ShootBullet(moveDirection.normalized);
            }
        }
        else
        {
            //trang thai follow nguoi choi
            transform.position += moveDirection.normalized * moveSpeed * dt;
        }

    }

    private void ShootBullet(Vector2 direction)
    {
        var skill = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        skill.CastSkill(direction);
    }
}
