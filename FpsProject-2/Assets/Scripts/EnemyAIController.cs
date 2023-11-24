using UnityEngine;

public class EnemyAIController : MonoBehaviour
{

    [SerializeField] private Transform projectileSpawn;
    [SerializeField] float damage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float gravity;

    private float attackTimer;
    private EnemyBulletSupply bulletSupply;

    private void Start()
    {
        bulletSupply = EnemyBulletSupply.Instance;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > attackCooldown)
        {
            var playerPos = PlayerMovementController.Instance.position;
            playerPos.y = projectileSpawn.position.y;
            var direction = playerPos - projectileSpawn.position;
            HandleShooting(direction);
        }
    }

    private void HandleShooting(Vector3 direction)
    {
        attackTimer = 0;
        var bullet = bulletSupply.GetSupply();
        bullet.transform.position = projectileSpawn.position;
        bullet.ShootProjectile(direction, projectileSpeed, damage, gravity);
    }
}



