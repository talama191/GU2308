using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private List<SkillCooldown> skillCooldowns;
    [SerializeField] private float attackRepeatCooldown;

    private GameManager gm => GameManager.Instance;
    private PlayerStat stat => PlayerStat.Instance;

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
        if (gm.gameState == GameState.GameOver)
        {

        }
        if (gm.gameState == GameState.Playing)
        {
            MoveControl();
            var enemy = EnemyManager.Instance.GetNearestEnemy(transform.position);
            if (enemy != null)
            {
                var direction = (Vector2)(enemy.transform.position - transform.position).normalized;
                foreach (var skillCooldown in skillCooldowns)
                {
                    skillCooldown.CooldownTimer += stat.IsAttackSpeedBuffed ? Time.deltaTime * 3 * stat.AttackRecoverySpeed
                                                                            : Time.deltaTime * stat.AttackRecoverySpeed;
                    if (skillCooldown.CooldownTimer >= skillCooldown.SkillInfo.Cooldown)
                    {
                        skillCooldown.CooldownTimer -= skillCooldown.SkillInfo.Cooldown;
                        for (int i = 0; i < stat.AttackRepeatCount; i++)
                        {
                            StartCoroutine(CastSkillDelay(attackRepeatCooldown * i, skillCooldown, direction));
                        }
                    }
                }
            }
        }
    }

    IEnumerator CastSkillDelay(float delay, SkillCooldown skillCooldown, Vector2 direction)
    {
        yield return new WaitForSeconds(delay);
        var skill = Instantiate(skillCooldown.SkillInfo.SkillPrefab,
                         transform.position,
                          Quaternion.identity);
        skill.CastSkill(direction);
    }

    private void MoveControl()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, moveY) * stat.Speed * Time.deltaTime;
    }
}

[Serializable]
public class SkillCooldown
{
    [SerializeField] SkillInfoBase skillInfo;
    public float CooldownTimer;

    public SkillInfoBase SkillInfo => skillInfo;


}

public enum GameState
{
    Playing,
    GameOver
}