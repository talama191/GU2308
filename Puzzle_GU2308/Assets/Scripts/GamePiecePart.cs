using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiecePart : MonoBehaviour
{
    [HideInInspector] public int X;
    [HideInInspector] public int Y;
    [HideInInspector] public GamePiece gamePiece;

    public void UpdatePosition(int x, int y)
    {
        GameBoard.Instance.SetOccupy(X, Y, this, gamePiece, false);
        X = x;
        Y = y;
        transform.position = new Vector2(x, y);
        GameBoard.Instance.SetOccupy(x, y, this, gamePiece);
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
