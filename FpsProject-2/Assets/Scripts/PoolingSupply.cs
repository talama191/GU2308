using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolingSupply<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T prefab;

    protected List<T> pool = new List<T>();

    public virtual T GetSupply()
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
