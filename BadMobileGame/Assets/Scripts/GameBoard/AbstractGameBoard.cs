using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGameBoard : MonoBehaviour, GameBoard
{
    private List<GameBoardPeice> pieces;
    [Header("Board Details")]
    public Transform initSpawnPosition; //where new shapes should spawn

    public virtual void Start()
    {
        pieces = new List<GameBoardPeice>();
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRandomShapeFromColor(ShapeColor.red);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach(GameBoardPeice b in GetAllPiecesOfType(ShapeType.square))
            {
                pieces.AddRange(b.MultiplyPiece(2));
            }
        }
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
}
