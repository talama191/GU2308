using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;

    protected float currentHP;
    protected float attackTimer = 0;

    private void Awake()
    {
        attackTimer = 0;
        currentHP = maxHp;
    }

    private void Update()
    {
        var dt = Time.deltaTime;
        attackTimer += dt;

        AIControl(dt);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            //Ke dich chet
            EnemyManager.Instance.RemoveEnemyFromList(this);
            Destroy(gameObject);
        }

    }
    protected abstract void AIControl(float dt);
}
