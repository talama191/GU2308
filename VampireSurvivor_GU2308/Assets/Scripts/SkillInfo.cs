using System;
using UnityEngine;

public abstract class SkillInfo : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] protected float cooldown;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private SkillBase skillPrefab;

    public float Damage => damage;
    public float Cooldown => cooldown;
    public SkillBase SkillPrefab => skillPrefab;
    public float ProjectileSpeed => projectileSpeed;
}
