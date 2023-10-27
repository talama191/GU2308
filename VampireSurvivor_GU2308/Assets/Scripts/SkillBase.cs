using System.Collections;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] protected SkillInfo skillInfo;
    protected Rigidbody2D rb;
    protected PlayerController pc => PlayerController.Instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public abstract void CastSkill(Vector2 direction);
}
