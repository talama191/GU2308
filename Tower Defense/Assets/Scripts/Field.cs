using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Field : MonoSingleton<Field>
{
    [Header("test")]
    [SerializeField] private Vector2Int start;
    [SerializeField] private Vector2Int end;

    private Dictionary<Vector2Int, FieldSlot> fieldSlots;
    private Dictionary<Vector2Int, FieldSlot> emptySlots;

    private void Awake()
    {
        UpdateFieldSlot();
        EventRegister.Instance.OnBuildTowerAction += OnBuildTowerAction;
    }

    private void OnDisable()
    {
        EventRegister.Instance.OnBuildTowerAction -= OnBuildTowerAction;
    }

    private void OnBuildTowerAction(string data)
    {
        UpdateFieldSlot();
        EventRegister.Instance.InvokeUpdatePath("");
    }

    public bool CheckIfSlotBlockOnlyPath(Vector2Int pos)
    {
        if (!fieldSlots.ContainsKey(pos)) return false;
        var emptySlotCopy = emptySlots.ToDictionary(e => e.Key, e => e.Value);
        emptySlotCopy.Remove(pos);
        if (BFSearch(EnemySpawner.Instance.StartSlot.Pos, EnemySpawner.Instance.EndSlot.Pos, emptySlotCopy) == null) return true;
        return false;
    }

    private void UpdateFieldSlot()
    {
        var allFieldSlots = gameObject.GetComponentsInChildren<FieldSlot>();
        fieldSlots = new Dictionary<Vector2Int, FieldSlot>();
        foreach (var slot in allFieldSlots)
        {
            var v2Pos = new Vector2Int(Mathf.RoundToInt(slot.transform.position.x), Mathf.RoundToInt(slot.transform.position.y));
            if (fieldSlots.ContainsKey(v2Pos))
            {
                DestroyImmediate(slot.gameObject);
                continue;
            }
            else
            {
                fieldSlots.Add(v2Pos, slot);
                slot.SetPos(v2Pos.x, v2Pos.y);
            }

            slot.gameObject.name = $"Slot {Mathf.RoundToInt(slot.transform.position.x)},{Mathf.RoundToInt(slot.transform.position.y)}";
        }
        UpdateSlots();
    }

    private void UpdateSlots()
    {
        emptySlots = fieldSlots.Where(s => !s.Value.IsOccupied).ToDictionary(s => s.Key, s => s.Value);
        foreach (var kvp in fieldSlots)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            var up = kvp.Key + Vector2Int.up;
            if (emptySlots.ContainsKey(up)) neighbors.Add(up);
            var right = kvp.Key + Vector2Int.right;
            if (emptySlots.ContainsKey(right)) neighbors.Add(right);
            var down = kvp.Key + Vector2Int.down;
            if (emptySlots.ContainsKey(down)) neighbors.Add(down);
            var left = kvp.Key + Vector2Int.left;
            if (emptySlots.ContainsKey(left)) neighbors.Add(left);

            kvp.Value.SetNeighbors(neighbors);
        }

    }

    public List<FieldSlot> BFSearchOnEmptyField(Vector2Int start, Vector2Int end)
    {
        return BFSearch(start, end, emptySlots);
    }

    private List<FieldSlot> BFSearch(Vector2Int start, Vector2Int end, Dictionary<Vector2Int, FieldSlot> emptySlots)
    {
        if (!emptySlots.ContainsKey(start) || !emptySlots.ContainsKey(end)) return null;
        if (emptySlots[start].IsOccupied || emptySlots[end].IsOccupied) return null;

        List<Vector2Int> visited = new List<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFroms = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == end)
            {
                return ReconstructedPath(start, end, cameFroms);
            }

            if (emptySlots.ContainsKey(current))
            {
                foreach (Vector2Int neighbor in emptySlots[current].Neighbors)
                {
                    if (neighbor == null) continue;
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        cameFroms[neighbor] = current;
                    }
                }
            }
        }

        return null;
    }

    private List<FieldSlot> ReconstructedPath(Vector2Int start, Vector2Int end, Dictionary<Vector2Int, Vector2Int> cameFroms)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = end;

        while (current != start)
        {
            path.Add(current);
            current = cameFroms[current];
        }

        path.Add(start);
        path.Reverse();

        List<FieldSlot> slotPath = new List<FieldSlot>();
        foreach (var p in path)
        {
            slotPath.Add(emptySlots[p]);
        }

        return slotPath;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 12;
        style.fontStyle = FontStyle.Bold;
        if (fieldSlots != null)
        {
            foreach (var kvp in fieldSlots)
            {
                if (!kvp.Value.IsOccupied) Handles.Label((Vector2)kvp.Key, $"{kvp.Key}", style);
            }
        }
    }
#endif
    #region Obsolete
    [Obsolete]
    private List<Vector2Int> DFSearch(Vector2Int start, Vector2Int end)
    {
        if (!fieldSlots.ContainsKey(start) || !fieldSlots.ContainsKey(end)) throw new System.Exception("No slot in field");

        List<Vector2Int> visited = new List<Vector2Int>();
        List<Vector2Int> paths = new List<Vector2Int>();

        if (DFSRecursive(visited, paths, start, end))
        {
            return paths;
        }

        return null;
    }

    [Obsolete]
    private bool DFSRecursive(List<Vector2Int> visited, List<Vector2Int> paths, Vector2Int current, Vector2Int end)
    {
        visited.Add(current);
        paths.Add(current);

        if (current == end)
        {
            return true;
        }

        if (fieldSlots.ContainsKey(current))
        {
            foreach (var neighbor in fieldSlots[current].Neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    if (DFSRecursive(visited, paths, neighbor, end))
                    {
                        return true;
                    }
                }
            }
        }
        paths.Remove(current);
        return false;
    }
    #endregion
}
