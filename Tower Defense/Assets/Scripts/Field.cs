using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Field : MonoSingleton<Field>
{
    [SerializeField] private GameObject tempPrefab;
    [SerializeField] private FieldSlot slotPrefab;

    [SerializeField] private Vector2Int origin;
    [SerializeField] private Vector2Int fieldSize;

    [Header("test")]
    [SerializeField] private Vector2Int start;
    [SerializeField] private Vector2Int end;

    private Dictionary<Vector2Int, FieldSlot> fieldSlots;
    private Dictionary<Vector2Int, FieldSlot> emptySlots;
    private List<GameObject> temps = new List<GameObject>();

    private void Awake()
    {
        CreateField();
    }

    private void CreateField()
    {
        fieldSlots = new Dictionary<Vector2Int, FieldSlot>();

        for (int x = origin.x; x < fieldSize.x + origin.x; x++)
        {
            for (int y = origin.y; y < fieldSize.y + origin.y; y++)
            {
                var slot = Instantiate(slotPrefab);
                slot.SetPos(x, y);
                fieldSlots.Add(new Vector2Int(x, y), slot);
            }
        }

        foreach (var kvp in fieldSlots)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            var up = kvp.Key + Vector2Int.up;
            if (fieldSlots.ContainsKey(up)) neighbors.Add(up);
            var right = kvp.Key + Vector2Int.right;
            if (fieldSlots.ContainsKey(right)) neighbors.Add(right);
            var down = kvp.Key + Vector2Int.down;
            if (fieldSlots.ContainsKey(down)) neighbors.Add(down);
            var left = kvp.Key + Vector2Int.left;
            if (fieldSlots.ContainsKey(left)) neighbors.Add(left);

            kvp.Value.SetNeighbors(neighbors);
        }
        UpdateSlots();
    }

    [ContextMenu("Update slot")]
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

    [ContextMenu("Find path")]
    private void FindPath()
    {

        var paths = BFSearch(start, end);
        for (int i = 0; i < temps.Count; i++)
        {
            Destroy(temps[i]);
        }
        temps.Clear();

        if (paths != null)
        {
            foreach (var node in paths)
            {
                var obj = Instantiate(tempPrefab);
                obj.transform.position = (Vector2)node;
                temps.Add(obj);
            }
        }
    }
    private List<Vector2Int> BFSearch(Vector2Int start, Vector2Int end)
    {
        if (!emptySlots.ContainsKey(start) || !emptySlots.ContainsKey(end)) throw new System.Exception("No slot in field");
        if (emptySlots[start].IsOccupied || emptySlots[end].IsOccupied) throw new System.Exception("Slot is occupied");

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

            foreach (Vector2Int neighbor in emptySlots[current].Neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    cameFroms[neighbor] = current;
                }
            }
        }

        return null;
    }

    private List<Vector2Int> ReconstructedPath(Vector2Int start, Vector2Int end, Dictionary<Vector2Int, Vector2Int> cameFroms)
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

        return path;
    }

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

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 22;
        style.fontStyle = FontStyle.Bold;
        if (fieldSlots != null)
        {
            foreach (var kvp in fieldSlots)
            {
                if (!kvp.Value.IsOccupied) Handles.Label((Vector2)kvp.Key, $"{kvp.Key}", style);

            }
        }
    }

}
