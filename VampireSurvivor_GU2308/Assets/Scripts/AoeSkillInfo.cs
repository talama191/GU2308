using UnityEngine;

[CreateAssetMenu(menuName = "SkillInfo/AoeSkill")]
public class AoeSkillInfo : SkillInfo
{
    [SerializeField] private float aoeRange;

    public float AoeRange => aoeRange;
}