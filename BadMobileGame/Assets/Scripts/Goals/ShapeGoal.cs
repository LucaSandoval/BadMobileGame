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

    protected GoalManager goalManager;

    public void Initialize(int max, GameBoard board, ShapeType type, ShapeColor color, GoalManager manager)
    {
        MaxPieces = max;
        CurrentPieces = 0;
        gameBoard = board;
        shapeType = type;
        shapeColor = color;
        goalManager = manager;
    }

    private void Update()
    {
        if (gameBoard != null)
        {
            CurrentPieces = gameBoard.GetAllSpecificPieces(shapeType, shapeColor).Count;

            if (IsComplete())
            {
                CompleteGoal();
            }
        }
    }

    public virtual void CompleteGoal()
    {
        if (goalManager.CurrentGoals.Contains(this))
        {
            goalManager.CurrentGoals.Remove(this);
        }
        Destroy(gameObject);
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
