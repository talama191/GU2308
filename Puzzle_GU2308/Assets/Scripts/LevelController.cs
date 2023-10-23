using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float stepCooldown;
    [SerializeField] private List<GamePiece> piecePrefabs;
    private GameBoard Board => GameBoard.Instance;

    private GamePiece activeGamePiece;

    private float stepTimer = 0;
    public static LevelController Instance;
    private void Awake()
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

    private void Start()
    {
        activeGamePiece = SpawnNewGamePiece();
    }

    private GamePiece SpawnNewGamePiece()
    {
        int randomIndex = Random.Range(0, piecePrefabs.Count);
        var spawn = GameBoard.Instance.SpawnOrigin;
        var newGamePiece = Instantiate(piecePrefabs[randomIndex], new Vector3(spawn.x, spawn.y, 0), Quaternion.identity);
        newGamePiece.SetupParts();
        return newGamePiece;
    }

    private void Update()
    {
        stepTimer += Time.deltaTime;
        if (stepTimer >= stepCooldown)
        {
            stepTimer = stepCooldown - stepTimer;
            Step();
        }
        MovePiece();
    }

    private void MovePiece()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var status = activeGamePiece.MovePiece(MoveDirection.Left);
            Debug.Log(status);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            var status = activeGamePiece.MovePiece(MoveDirection.Right);
            Debug.Log(status);
        }
    }

    private void Step()
    {
        var success = activeGamePiece.MovePiece(MoveDirection.Down);
        if (!success)
        {
            activeGamePiece = SpawnNewGamePiece();
            CheckLines();
        }

    }

    private void CheckLines()
    {
        for (int i = 0; i < Board.Height; i++)
        {
            var slots = new List<GameBoardSlot>();
            for (int j = 0; j < Board.Width; j++)
            {
                slots.Add(Board.GetSlot(Board.Origin.x + j, Board.Origin.y - i));
            }
            var allOccupied = slots.All(s => s.IsOccupied);
            if (allOccupied)
            {
                foreach (var slot in slots)
                {
                    var part = slot.part;
                    Board.SetOccupy(slot.X, slot.Y, slot.part, null, false);
                    part.SelfDestroy();
                }
                MoveDownAllLineFrom(Board.Origin.y - i);
                CheckLines();
                return;
            }

        }
    }

    private void MoveDownAllLineFrom(int startLine)
    {
        for (int i = startLine + 1; i < Board.Origin.y + 1; i++)
        {
            var slots = new List<GameBoardSlot>();
            for (int j = 0; j < Board.Width; j++)
            {
                slots.Add(Board.GetSlot(Board.Origin.x + j, i));
            }
            foreach (var slot in slots)
            {
                if (slot.IsOccupied)
                {
                    Board.SetOccupy(slot.X, slot.Y - 1, slot.part, slot.gamePiece);
                    slot.part.UpdatePosition(slot.X, slot.Y - 1);
                    Board.SetOccupy(slot.X, slot.Y, null, null, false);
                }
            }
        }
    }
}

