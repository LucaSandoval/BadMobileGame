using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGameBoard : MonoBehaviour, GameBoard
{
    private List<GameBoardPeice> pieces;
    [Header("Board Details")]
    public Transform initSpawnPosition; //where new shapes should spawn

    public virtual void Awake()
    {
        pieces = new List<GameBoardPeice>();
    }

    public virtual void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AddRandomShapeFromColor(ShapeColor.red);
        //    AddRandomColorFromShape(ShapeType.triangle);
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    //RemoveRandomPiece();
        //    //RemoveSpecificPieces(GetAllPieces());
        //    RemoveRandomColorOfShape(ShapeType.triangle);
        //}
    }

    public void AddRandomColorFromShape(ShapeType type)
    {
        ShapeColor randColor = (ShapeColor)Random.Range(0, System.Enum.GetValues(typeof(ShapeColor)).Length);
        AddShapeToBoard(randColor, type);
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
        newPiece.PutInGameBoard(this);
    }

    public List<GameBoardPeice> GetAllPieces()
    {
        return pieces;
    }

    public List<GameBoardPeice> GetAllPiecesOfColor(ShapeColor color)
    {

        List<GameBoardPeice> colorList = new List<GameBoardPeice>();
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i] is BasicShape)
            {
                BasicShape shape = (BasicShape)pieces[i];
                if (shape.GetShapeColor() == color)
                {
                    colorList.Add(pieces[i]);
                }
            }
        }

        return colorList;
    }

    public List<GameBoardPeice> GetAllPiecesOfType(ShapeType type)
    {
        List<GameBoardPeice> shapeList = new List<GameBoardPeice>();
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i] is BasicShape)
            {
                BasicShape shape = (BasicShape)pieces[i];
                if (shape.GetShapeType() == type)
                {
                    shapeList.Add(pieces[i]);
                }
            }
        }

        return shapeList;
    }

    public int GetTotalPieces()
    {
        return (pieces != null) ? pieces.Count : 0;
    }

    public void RegisterMultipleNewPeices(List<GameBoardPeice> newPieces)
    {
        foreach(GameBoardPeice p in newPieces)
        {
            //GameObject test = p.test();
            if (!pieces.Contains(p))
            {
                pieces.Add(p);
                p.PutInGameBoard(this);
            }
        }
    }

    public void RemoveRandomPiece()
    {
        if (GetTotalPieces() > 0)
        {
            RemovePieceHelper(pieces[Random.Range(0, pieces.Count)]);
        }    
    }

    public void RemoveSpecificPieces(List<GameBoardPeice> pieces)
    {
        for(int i = pieces.Count - 1; i >= 0; i--)
        {
            RemovePieceHelper(pieces[i]);
        }
    }

    public void RemoveRandomShapeOfColor(ShapeColor color)
    {
        List<GameBoardPeice> piecesOfColor = GetAllPiecesOfColor(color);
        if (piecesOfColor.Count > 0)
        {
            RemovePieceHelper(piecesOfColor[Random.Range(0, piecesOfColor.Count)]);
        }     
    }

    public void RemoveRandomColorOfShape(ShapeType type)
    {
        List<GameBoardPeice> piecesOfShape = GetAllPiecesOfType(type);
        if (piecesOfShape.Count > 0)
        {
            RemovePieceHelper(piecesOfShape[Random.Range(0, piecesOfShape.Count)]);
        }
    }

    //Avoids code duplication, not a part of the interface
    private void RemovePieceHelper(GameBoardPeice piece)
    {
        pieces.Remove(piece);
        piece.SetFallingState();    
    }

    public void RemoveSpecificPiece(GameBoardPeice piece)
    {
        if (pieces.Contains(piece))
        {
            pieces.Remove(piece);
        }  
    }

    public void AddRandomShapeToBoard()
    {
        ShapeColor randColor = (ShapeColor)Random.Range(0, System.Enum.GetValues(typeof(ShapeColor)).Length);
        ShapeType randType = (ShapeType)Random.Range(0, System.Enum.GetValues(typeof(ShapeType)).Length);

        AddShapeToBoard(randColor, randType);
    }
}
