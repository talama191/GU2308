using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHp = 10;
    [SerializeField] private float flashingDuration;
    [SerializeField] private Color flashColor;

    private Material material;
    private float hp = 0;
    private Coroutine flashCoroutine;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        SetupDamageable();
    }

    public void SetupDamageable()
    {
        hp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        flashCoroutine = StartCoroutine(PlayEffect());
        if (hp <= 0)
        {
            TriggerDeath();
        }
    }

    IEnumerator PlayEffect()
    {
        float flashingTimer = 0;
        material.SetColor("_FlashingColor", flashColor);
        while (flashingTimer < flashingDuration)
        {
            flashingTimer += Time.deltaTime;
            material.SetFloat("_FlashingAmount", (flashingDuration - flashingTimer) / flashingDuration);

            yield return null;
        }
        material.SetFloat("_FlashingAmount", 0);
    }

    private void TriggerDeath()
    {
        gameObject.SetActive(false);
    }

}
