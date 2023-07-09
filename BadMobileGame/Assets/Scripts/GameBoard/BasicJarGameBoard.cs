using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicJarGameBoard : MonoBehaviour, GameBoard
{
    public List<GameBoardPeice> pieces;
    [Header("Board Details")]
    public Transform initSpawnPosition; //where new shapes should spawn

    void Start()
    {
        pieces = new List<GameBoardPeice>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRandomShapeFromColor(ShapeColor.red);
        }
    }

    public void AddRandomColorFromShape(ShapeType type)
    {
        throw new System.NotImplementedException();
    }

    public void AddRandomShapeFromColor(ShapeColor color)
    {
        ShapeType randType = (ShapeType)Random.Range(0, System.Enum.GetValues(typeof(ShapeType)).Length);
        AddShapeToBoard(color, randType);
    }

    public void AddShapeToBoard(ShapeColor color, ShapeType type)
    {
        GameBoardPeice newPiece = GameEntityController.SpawnShape(type, color);
        newPiece.SetPosition(initSpawnPosition.position);
        pieces.Add(newPiece);
    }

    public List<GameBoardPeice> GetAllPieces()
    {
        throw new System.NotImplementedException();
    }

    public List<GameBoardPeice> GetAllPiecesOfColor(ShapeColor color)
    {
        throw new System.NotImplementedException();
    }

    public List<GameBoardPeice> GetAllPiecesOfType(ShapeType type)
    {
        throw new System.NotImplementedException();
    }

    public int GetTotalPieces()
    {
        return (pieces != null) ? pieces.Count : 0;
    }
}
