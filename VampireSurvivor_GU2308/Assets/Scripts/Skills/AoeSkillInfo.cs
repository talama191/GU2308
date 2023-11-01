using UnityEngine;

[CreateAssetMenu(menuName = "SkillInfo/AoeSkill")]
public class AoeSkillInfo : SkillInfoBase
{
    [SerializeField] private float aoeRange;

    public float AoeRange => aoeRange;
}