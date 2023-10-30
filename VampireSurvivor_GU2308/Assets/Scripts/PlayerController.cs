using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    [SerializeField] private float maxHp;
    [SerializeField] private float speed;
    // [SerializeField] private List<SkillInfo> skillInfos;
    [SerializeField] private List<SkillCooldown> skillCooldowns;
    private float currentHP;
    private GameState gameState;

    public float Speed => isSpeedBuffed ? speed * 1.5f : speed;
    private float attackSpeedBuffTimer = 0f;
    private bool isAttackSpeedBuffed = false;
    private float speedBuffTimer = 0f;
    private bool isSpeedBuffed = false;


    public void HealPlayer(float healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHp) currentHP = maxHp;
        UiManager.Instance.UpdateHP(currentHP / maxHp);
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        UiManager.Instance.UpdateHP(currentHP / maxHp);
        if (currentHP <= 0) gameState = GameState.GameOver;
    }

    public void BoostMoveSpeed(float duration)
    {
        isSpeedBuffed = true;
        speedBuffTimer = 60f;
    }

    public void BoostAttackSpeed(float duration)
    {
        isAttackSpeedBuffed = true;
        attackSpeedBuffTimer = 60f;
    }

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
            attackSpeedBuffTimer -= Time.deltaTime;
            speedBuffTimer -= Time.deltaTime;
            if (attackSpeedBuffTimer <= 0)
            {
                isAttackSpeedBuffed = false;
            }
            if (speedBuffTimer <= 0)
            {
                isSpeedBuffed = false;
            }
            var enemy = EnemyManager.Instance.GetNearestEnemy(transform.position);
            if (enemy != null)
            {
                var direction = (Vector2)(enemy.transform.position - transform.position).normalized;
                foreach (var skillCooldown in skillCooldowns)
                {
                    skillCooldown.CooldownTimer += isAttackSpeedBuffed ? Time.deltaTime * 3 : Time.deltaTime;
                    if (skillCooldown.CooldownTimer >= skillCooldown.SkillInfo.Cooldown)
                    {
                        skillCooldown.CooldownTimer -= skillCooldown.SkillInfo.Cooldown;
                        var skill = Instantiate(skillCooldown.SkillInfo.SkillPrefab,
                         transform.position,
                          Quaternion.identity);
                        skill.CastSkill(direction);
                    }
                }
            }
        }
    }

    private void MoveControl()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, moveY) * Speed * Time.deltaTime;
    }

}

[Serializable]
public class SkillCooldown
{
    [SerializeField] SkillInfo skillInfo;
    public float CooldownTimer;

    public SkillInfo SkillInfo => skillInfo;


}

public enum GameState
{
    Playing,
    GameOver
}