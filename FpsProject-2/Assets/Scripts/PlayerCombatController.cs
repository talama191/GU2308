using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] BulletProjectile bulletPrefab;
    [SerializeField] Transform projectileSpawn;

    [Header("Stats")]
    [SerializeField] private float damage = 5;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float shootCooldown = 0.2f;

    private float shootTimer;

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && shootTimer <= 0f)
        {
            shootTimer = shootCooldown;
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = projectileSpawn.position;
            bullet.ShootProjectile(projectileSpawn.forward, projectileSpeed, damage, gravity);
        }
    }
}
