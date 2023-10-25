using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private float maxHp;
    [SerializeField] private Skill1Bullet skill1Prefab;
    [SerializeField] private float speed;
    [SerializeField] public float skill1AttackCooldown;
    public float skill1Damage;
    private float currentHP;
    private float skill1AttackTimer;
    private GameState gameState;

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
        DontDestroyOnLoad(gameObject);
        gameState = GameState.Playing;
    }
    private void Start()
    {
        currentHP = maxHp;
    }

    void Update()
    {
        if (gameState == GameState.GameOver)
        {

        }
        if (gameState == GameState.Playing)
        {
            MoveControl();
            CastSkill1();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        UiManager.Instance.UpdateHP(currentHP / maxHp);
        if (currentHP <= 0) gameState = GameState.GameOver;
    }

    private void CastSkill1()
    {
        skill1AttackTimer += Time.deltaTime;
        if (skill1AttackTimer >= skill1AttackCooldown)
        {
            skill1AttackTimer = 0;
            var enemy = EnemyManager.Instance.GetNearestEnemy(transform.position);
            if (enemy != null)
            {
                var bullet = Instantiate(skill1Prefab, transform.position, Quaternion.identity);
                var direction = (Vector2)(enemy.transform.position - transform.position).normalized;
                bullet.ShootBullet(direction);
            }
        }
    }

    private void MoveControl()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, moveY) * speed * Time.deltaTime;
    }
}

public enum GameState
{
    Playing,
    GameOver
}