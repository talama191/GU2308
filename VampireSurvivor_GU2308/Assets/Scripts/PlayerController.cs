using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private Skill1Bullet skill1Prefab;
    [SerializeField] private float speed;
    public float skill1Damage;
    [SerializeField] public float skill1AttackCooldown;
    private float skill1AttackTimer;

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
    }

    void Update()
    {
        MoveControl();
        CastSkill1();
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
