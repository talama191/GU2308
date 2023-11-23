using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    [SerializeField] private ParticleSystem effectPrefab;

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
    }

    public void SpawnEffect(float scale, Vector3 origin)
    {
        float realScale = scale / 2;
        var effect = Instantiate(effectPrefab);
        effect.transform.localScale = Vector3.one * realScale;
        effect.transform.position = origin;
        StartCoroutine(DespawnEffect(effect.gameObject));
    }

    IEnumerator DespawnEffect(GameObject objectToDespawn)
    {
        yield return new WaitForSeconds(1f);
        objectToDespawn.SetActive(false);
    }
}
