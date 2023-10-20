using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float currentHP;

    private void Awake()
    {
        currentHP = maxHp;
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
}
