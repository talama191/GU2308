﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float currentHp;
    private EnemyData data;
    private FieldSlot occupySlot;
    private FieldSlot endSlot;
    private List<FieldSlot> path;
    private int pathIndex = 0;
    private int lengthToEnd = 0;
    private EventRegister eventRegister;

    public EnemyData Data => data;

    private void Update()
    {
        if (pathIndex < path.Count)
        {
            var targetPos = path[pathIndex];
            var dir = targetPos.transform.position - transform.position;
            if (dir.sqrMagnitude <= 0.05f)
            {
                occupySlot = path[pathIndex];
                pathIndex++;
            }
            transform.position += dir.normalized * data.Speed * Time.deltaTime;
        }
        else
        {
            path.Clear();
        }

    }

    private void UpdatePath(Vector2Int start, Vector2Int end)
    {
        pathIndex = 1;
        path = Field.Instance.BFSearchOnEmptyField(start, end);

    }

    private void OnUpdatePath(string data)
    {
        UpdatePath(occupySlot.Pos, endSlot.Pos);
    }

    public void SetupData(EnemyData data, FieldSlot startSlot, FieldSlot endSlot)
    {
        this.data = data;
        currentHp = data.Hp;
        occupySlot = startSlot;
        this.endSlot = endSlot;
        UpdatePath(startSlot.Pos, endSlot.Pos);
        eventRegister = EventRegister.Instance;
        eventRegister.OnUpdatePath += OnUpdatePath;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            eventRegister.OnUpdatePath -= OnUpdatePath;
            Destroy(gameObject);
        }
    }


}
