using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FieldSlot : MonoBehaviour
{
    [SerializeField] private bool isOccupied;
    private Vector2Int pos;
    private List<Vector2Int> neighbors;

    public Vector2Int Pos => pos;
    public List<Vector2Int> Neighbors => neighbors;
    public bool IsOccupied => isOccupied;

    public void SetPos(int x, int y)
    {
        pos = new Vector2Int(x, y);
        transform.position = (Vector2)pos;
    }

    public void SetNeighbors(List<Vector2Int> neighbors)
    {
        this.neighbors = neighbors;
    }

}
