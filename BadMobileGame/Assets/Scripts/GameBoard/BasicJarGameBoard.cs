using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicJarGameBoard : MonoBehaviour, GameBoard
{
    public List<GameBoardPeice> pieces;

    void Start()
    {
        pieces = new List<GameBoardPeice>();
    }

    void Update()
    {

    }

    public void AddRandomColorFromShape(ShapeType type)
    {
        throw new System.NotImplementedException();
    }

    public void AddRandomShapeFromColor(ShapeColor color)
    {
        throw new System.NotImplementedException();
    }

    public void AddShapeToBoard(ShapeColor color, ShapeType type)
    {
        GameBoardPeice newPiece = GameEntityController.SpawnShape(type, color);
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
        throw new System.NotImplementedException();
    }
}
