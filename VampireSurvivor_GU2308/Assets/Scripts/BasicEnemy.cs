public class BasicEnemy : EnemyBase
{
    protected override void AIControl(float dt)
    {
        var playerPos = PlayerController.Instance.transform.position;
        var moveDirection = playerPos - transform.position;
        var distance = moveDirection.magnitude;
        if (distance > 0.1f)
        {
            transform.position += moveDirection.normalized * moveSpeed * dt;
        }
        if (distance <= attackRange)
        {
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0;
                PlayerController.Instance.TakeDamage(attackDamage);
            }
        }
    }
}
