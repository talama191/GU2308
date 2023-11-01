using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float maxHp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;

    private SpriteRenderer rdr;
    public float MoveSpeed => moveSpeed;
    protected float currentHP;
    protected float attackTimer = 0;

    private void Awake()
    {
        attackTimer = 0;
        currentHP = maxHp;
        rdr = GetComponent<SpriteRenderer>();
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
        DOTween.ToAlpha(() => rdr.color, x => rdr.color = x, 0.6f, 0.005f)
        .SetLoops(3, LoopType.Yoyo)
        .SetEase(Ease.InBounce);
        if (currentHP <= 0)
        {
            //Ke dich chet
            OnDead();
        }
    }

    private float[] randomItemWeights = new float[] { 30, 30, 40, 300 };
    [SerializeField] private List<BonusItem> itemDrops;

    private void OnDead()
    {
        EnemyManager.Instance.RemoveEnemyFromList(this);
        var index = RandomItem();
        SpawnItem(index);

        PlayerStat.Instance.AddEXP(30);
        Destroy(gameObject);
    }

    private void SpawnItem(int index)
    {
        if (randomItemWeights.Count() - 1 <= index) return;
        Instantiate(itemDrops[index], transform.position, Quaternion.identity);
    }

    private int RandomItem()
    {
        float weightedSum = randomItemWeights.Sum();
        float randomRoll = Random.Range(0, weightedSum);

        float currentSum = 0;
        for (int i = 0; i < randomItemWeights.Count(); i++)
        {
            currentSum += randomItemWeights[i];
            if (currentSum > randomRoll)
            {
                return i;
            }
        }
        return 0;
    }


    protected abstract void AIControl(float dt);
}
