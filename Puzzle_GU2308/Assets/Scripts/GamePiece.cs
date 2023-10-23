using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    [SerializeField] private List<GamePiecePart> parts;
    // [SerializeField] private GamePiecePart 

    public List<GamePiecePart> Parts => parts;
    private GameBoard board => GameBoard.Instance;

    public void SetupParts()
    {
        foreach (var part in parts)
        {
            part.X = (int)part.transform.position.x;
            part.Y = (int)part.transform.position.y;
            part.gamePiece = this;
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
public enum MoveDirection
{
    Down,
    Left,
    Right
}