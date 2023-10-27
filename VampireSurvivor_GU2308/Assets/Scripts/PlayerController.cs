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
            var enemy = EnemyManager.Instance.GetNearestEnemy(transform.position);
            if (enemy != null)
            {
                var direction = (Vector2)(enemy.transform.position - transform.position).normalized;
                foreach (var skillCooldown in skillCooldowns)
                {
                    skillCooldown.CooldownTimer += Time.deltaTime;
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

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        UiManager.Instance.UpdateHP(currentHP / maxHp);
        if (currentHP <= 0) gameState = GameState.GameOver;
    }


    private void MoveControl()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, moveY) * speed * Time.deltaTime;
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