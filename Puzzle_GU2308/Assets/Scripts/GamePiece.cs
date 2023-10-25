using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePiece : MonoBehaviour
{
    [SerializeField] private List<GamePiecePart> parts;
    [SerializeField] private GamePiecePart centerPart;
    [SerializeField] private List<GamePieceRotationState> states;

    // public List<GamePiecePart> Parts => parts;
    private GameBoard board => GameBoard.Instance;
    private int stateIndex = 0;

    private void Awake()
    {
        foreach (var state in states)
        {
            state.SetupData();
        }
    }

    public void SetupParts()
    {
        foreach (var part in parts)
        {
            part.X = (int)part.transform.position.x;
            part.Y = (int)part.transform.position.y;
            part.gamePiece = this;
        }
    }

    public void Rotate()
    {
        if (states == null) return;
        if (states.Count <= 0) return;
        stateIndex++;
        if (stateIndex >= states.Count) stateIndex = 0;
        var state = states[stateIndex];
        bool canRotate = true;

        foreach (KeyValuePair<GamePiecePart, Vector2Int> kvp in state.partOffsetDict)
        {
            var v2Int = new Vector2Int(centerPart.X + kvp.Value.x, centerPart.Y + kvp.Value.y);
            if (v2Int.x <= board.LeftBorder
            || v2Int.x >= board.RightBorder
            || v2Int.y <= board.BottomBorder)
            {
                canRotate = false;
                stateIndex--;
                if (stateIndex < 0)
                {
                    stateIndex = states.Count - 1;
                }
                return;
            }
        }

        foreach (KeyValuePair<GamePiecePart, Vector2Int> kvp in state.partOffsetDict)
        {
            var part = kvp.Key;
            part.UpdatePosition(centerPart.X + kvp.Value.x, centerPart.Y + kvp.Value.y);
        }
    }

    public bool MovePiece(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Down:
                return MoveAllPart(0, -1, direction);
            case MoveDirection.Left:
                MoveAllPart(-1, 0, direction);
                break;
            case MoveDirection.Right:
                MoveAllPart(1, 0, direction);
                break;
        }
        return true;
    }
    public bool MoveAllPart(int moveX, int moveY, MoveDirection direction)
    {
        bool canMove = true;
        foreach (var part in parts)
        {
            switch (direction)
            {
                case MoveDirection.Down:
                    if (part.Y - 1 <= board.BottomBorder)
                    {
                        canMove = false;
                        return canMove;
                    }
                    if (board.IsOccupied(part.X, part.Y - 1, this))
                    {
                        return false;
                    }
                    break;
                case MoveDirection.Left:
                    if (part.X - 1 <= board.LeftBorder)
                    {
                        canMove = false;
                        return canMove;
                    }
                    if (board.IsOccupied(part.X - 1, part.Y, this))
                    {
                        return false;
                    }
                    break;
                case MoveDirection.Right:
                    if (part.X + 1 >= board.RightBorder)
                    {
                        canMove = false;
                        return canMove;
                    }
                    if (board.IsOccupied(part.X + 1, part.Y, this))
                    {
                        return false;
                    }
                    break;
            }
        }
        if (canMove)
        {
            foreach (var part in parts)
            {
                part.UpdatePosition(part.X + moveX, part.Y + moveY);
            }
        }
        return canMove;
    }
}

[Serializable]
public class GamePieceRotationState
{
    [SerializeField] private List<GamePiecePart> parts;
    [SerializeField] private List<Vector2Int> offsets;
    public Dictionary<GamePiecePart, Vector2Int> partOffsetDict;

    public void SetupData()
    {
        if (parts.Count != offsets.Count) throw new Exception("parts and offsets should have the same length");
        partOffsetDict = new Dictionary<GamePiecePart, Vector2Int>();
        for (int i = 0; i < parts.Count; i++)
        {
            partOffsetDict.Add(parts[i], offsets[i]);
        }
    }
}

public enum MoveDirection
{
    Down,
    Left,
    Right
}