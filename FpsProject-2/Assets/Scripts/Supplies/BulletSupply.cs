

public class BulletSupply : PoolingSupply<BulletProjectile>
{
    public static BulletSupply Instance;

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
