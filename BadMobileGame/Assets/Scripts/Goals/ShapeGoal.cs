using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGoal : MonoBehaviour, GameGoal
{
    protected int MaxPieces;
    protected int CurrentPieces;

    protected GameBoard gameBoard;

    protected ShapeType shapeType;
    protected ShapeColor shapeColor;

    public void Initialize(int max, GameBoard board, ShapeType type, ShapeColor color)
    {
        MaxPieces = max;
        CurrentPieces = 0;
        gameBoard = board;
        shapeType = type;
        shapeColor = color;
    }

    private void Update()
    {
        if (gameBoard != null)
        {
            //gameBoard.get

            if (IsComplete())
            {
                CompleteGoal();
            }
        }
    }

    public virtual void CompleteGoal()
    {
        //idk what to put here yet
    }

    public virtual bool IsComplete()
    {
        return CurrentPieces >= MaxPieces;
    }

    public int GetCurrentProgress()
    {
        return CurrentPieces;
    }

    public int GetMaxProgress()
    {
        return MaxPieces;
    }

    public ShapeType GetShapeType()
    {
        return shapeType;
    }

    public ShapeColor GetShapeColor()
    {
        return shapeColor;
    }
}
