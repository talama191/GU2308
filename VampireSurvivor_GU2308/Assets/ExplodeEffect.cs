using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
