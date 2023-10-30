using UnityEngine;

public class BonusItem : MonoBehaviour
{
    [SerializeField] private float hpHealAmount;
    [SerializeField] private bool isSpeedBoost;
    [SerializeField] private float speedBoostAmount;
    [SerializeField] private bool isAttackSpeedBoost;
    [SerializeField] private float attackSpeedBoostAmount;

    [SerializeField] private float duration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isSpeedBoost) PlayerController.Instance.BoostMoveSpeed(60f);
            if (isAttackSpeedBoost) PlayerController.Instance.BoostAttackSpeed(60f);
            PlayerController.Instance.HealPlayer(15f);
            Destroy(gameObject);
        }
    }
}
