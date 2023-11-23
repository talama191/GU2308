using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolingSupply<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;

    private List<T> pool = new List<T>();

    public T GetSupply()
    {
        var supply = pool.FirstOrDefault(t => !t.gameObject.activeInHierarchy);
        if (supply == null)
        {
            supply = Instantiate(prefab);
            pool.Add(supply);
        }
        supply.gameObject.SetActive(true);
        return supply;
    }
}
