using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] BulletSupply bulletSupply;
    [SerializeField] ExplosiveBulletSupply explosiveBulletSupply;
    [SerializeField] Transform projectileSpawn;

    [Header("Stats")]
    [SerializeField] private float damage = 5;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float shootCooldown = 0.2f;
    [Header("Explosive Projectile")]
    [SerializeField] private float explosionRadius = 10;
    [SerializeField] private LayerMask impactLayerMask;

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
            var bullet = bulletSupply.GetSupply();
            bullet.transform.position = projectileSpawn.position;
            bullet.ShootProjectile(projectileSpawn.forward, projectileSpeed, damage, gravity);
        }
        if (Input.GetMouseButtonDown(1) && shootTimer <= 0f)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, impactLayerMask))
            {
                var originalDir = (hit.point - projectileSpawn.position);
                float angle;
                bool canHit = CalculateTrajectory(originalDir.magnitude, projectileSpeed, out angle);
                var rotation = Quaternion.AngleAxis(-angle, projectileSpawn.transform.right);
                var finalDir = rotation * originalDir;

                shootTimer = shootCooldown;
                var bullet = explosiveBulletSupply.GetSupply();
                bullet.transform.position = projectileSpawn.position;
                bullet.ShootProjectile(finalDir.normalized, projectileSpeed, damage, gravity, true, explosionRadius);
            }


        }
    }

    public static bool CalculateTrajectory(float TargetDistance, float ProjectileVelocity, out float CalculatedAngle)
    {
        CalculatedAngle = 0.5f *
            (Mathf.Asin((-Physics.gravity.y * TargetDistance) / (ProjectileVelocity * ProjectileVelocity)) * Mathf.Rad2Deg);
        if (float.IsNaN(CalculatedAngle))
        {
            CalculatedAngle = 30;
            return false;
        }
        return true;
    }
}
