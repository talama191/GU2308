using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FieldSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isOccupied;
    [SerializeField] private SpriteRenderer towerSprite;
    private Vector2Int pos;
    private List<Vector2Int> neighbors;
    public bool isBuildable = true;

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

    public void BuildTower(Sprite towerSprite)
    {
        isOccupied = true;
        this.towerSprite.sprite = towerSprite;
    }

    public void EnemyOccupySlot(Enemy enemy)
    {

    }

    public void SetOccupy(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isBuildable)
        {
            UIController.Instance.OpenBuildSheetPopup(this);
        }
    }
}
