using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    private bool isDead = true;

    public bool IsDead => isDead;

    public void SetAliveState()
    {
        isDead = false;
    }
}

