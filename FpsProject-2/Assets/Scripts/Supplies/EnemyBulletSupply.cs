

public class EnemyBulletSupply : PoolingSupply<BulletProjectile>
{
    public static EnemyBulletSupply Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}