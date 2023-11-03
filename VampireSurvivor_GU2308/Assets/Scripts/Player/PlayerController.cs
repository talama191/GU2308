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

    [SerializeField] private float splitSpacing;

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
                var origins = new List<Vector2>();
                var direction = (Vector2)(enemy.transform.position - transform.position).normalized;

                origins.Add((Vector2)transform.position);
                var splitCount = stat.AttackSplitCount;
                for (int i = 1; i < splitCount; i++)
                {
                    if (i % 2 != 0)
                    {
                        int splitIndex = Mathf.FloorToInt(-(i / 2)) - 1;
                        var spacing = splitIndex * splitSpacing;
                        var perpendicular = Vector2.Perpendicular(direction).normalized * spacing;
                        var origin = (Vector2)transform.position + perpendicular;
                        origins.Add(origin);
                    }
                    else
                    {
                        int splitIndex = Mathf.FloorToInt((i / 2));
                        var spacing = splitIndex * splitSpacing;
                        var perpendicular = Vector2.Perpendicular(direction).normalized * spacing;
                        var origin = (Vector2)transform.position + perpendicular;
                        origins.Add(origin);
                    }

                }

                foreach (var skillCooldown in skillCooldowns)
                {
                    skillCooldown.CooldownTimer += stat.IsAttackSpeedBuffed ? Time.deltaTime * 3 * stat.AttackRecoverySpeed
                                                                            : Time.deltaTime * stat.AttackRecoverySpeed;
                    if (skillCooldown.CooldownTimer >= skillCooldown.SkillInfo.Cooldown)
                    {
                        skillCooldown.CooldownTimer -= skillCooldown.SkillInfo.Cooldown;
                        foreach (var origin in origins)
                        {

                            for (int i = 0; i < stat.AttackRepeatCount; i++)
                            {
                                StartCoroutine(CastSkillDelay(attackRepeatCooldown * i, skillCooldown, direction, origin));
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator CastSkillDelay(float delay, SkillCooldown skillCooldown, Vector2 direction, Vector2 skillOrigin)
    {
        yield return new WaitForSeconds(delay);
        var skill = Instantiate(skillCooldown.SkillInfo.SkillPrefab,
                         skillOrigin,
                          Quaternion.identity);
        skill.CastSkill(direction);
    }

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashTimer = 0;
    private float dashCooldownTimer = 0;
    private bool isDashing = false;
    private void MoveControl()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, moveY) * stat.Speed * Time.deltaTime;

        dashCooldownTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        if (dashTimer > dashDuration)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
        }
        if (moveX != 0 || moveY != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer >= dashCooldown)
            {
                isDashing = true;
                dashTimer = 0;
                dashCooldownTimer = 0;
                var dashDirection = new Vector2(moveX, moveY).normalized;
                rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
            }
        }
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