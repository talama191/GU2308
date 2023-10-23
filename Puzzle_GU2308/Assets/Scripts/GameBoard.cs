using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public static GameBoard Instance;
    [SerializeField] private Vector2Int origin;
    [SerializeField] private Vector2Int spawnOrigin;
    [SerializeField] private int width;
    [SerializeField] private int height;

    private List<GameBoardSlot> slots = new List<GameBoardSlot>();
    public Vector2Int SpawnOrigin => spawnOrigin;
    public Vector2Int Origin => origin;
    public int LeftBorder => origin.x - 1;
    public int RightBorder => origin.x + width;
    public int BottomBorder => origin.y - height;
    public int Width => width;
    public int Height => height;

    private void Awake()
    {
        InitSingleTon();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                slots.Add(new GameBoardSlot(origin.x + i, origin.y - j));
            }
        }
    }

    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnDrawGizmos()
    {
        foreach (var slot in slots)
        {
            Handles.Label(new Vector3(slot.X, slot.Y, 0), $"{slot.X},{slot.Y}");
            Handles.Label(new Vector3(slot.X, slot.Y - 0.2f, 0), $"{slot.IsOccupied}");
        }
    }

    public bool IsOccupied(int x, int y, GamePiece gamePiece)
    {
        var slot = slots.FirstOrDefault(s => s.X == x && s.Y == y);
        if (slot == null) return false;
        if (slot.gamePiece == gamePiece) return false;
        return slot.IsOccupied;
    }

    public GameBoardSlot GetSlot(int x, int y)
    {
        var slot = slots.FirstOrDefault(s => s.X == x && s.Y == y);
        if (slot == null) Debug.Log($"slot: {x} {y}");
        return slot;
    }

    public void SetOccupy(int x, int y, GamePiecePart part, GamePiece gamePiece, bool isSetting = true)
    {
        var slot = slots.FirstOrDefault(s => s.X == x && s.Y == y);
        if (slot == null) return;
        if (!isSetting)
        {
            if (part != slot.part)
            {
                return;
            }
            slot.IsOccupied = false;
            slot.part = null;
            slot.gamePiece = null;
            return;
        }
        slot.IsOccupied = true;
        slot.part = part;
        slot.gamePiece = gamePiece;
    }

}

public class GameBoardSlot
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool IsOccupied;
    public GamePiecePart part;
    public GamePiece gamePiece;

    public GameBoardSlot(int x, int y)
    {
        this.X = x;
        this.Y = y;
        IsOccupied = false;

    }
}

