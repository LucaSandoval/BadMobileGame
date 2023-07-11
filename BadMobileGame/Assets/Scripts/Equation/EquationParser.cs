using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationParser : MonoBehaviour
{
    public AbstractGameBoard gameBoard;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ParseEquation(new EquationNumber(3), 
                new EquationExpression(MathSymbol.multiply), 
                new EquationShapeType(ShapeType.square));
        }
    }

    public void ParseEquation(EquationSymbol operand1, EquationExpression expression, EquationSymbol operand2)
    {
        //Failure cases
        if (!(operand1 is EquationNumber) && !(operand2 is EquationNumber)) //both aren't numbers
        {
            return;
        }


        if (operand1 is EquationNumber && operand2 is EquationNumber)
        {
            //Operation on numbers
        } else
        {
            //Get the number. At this point there must be one.
            int number = GetNumberFromOperands(operand1, operand2);

            if (expression.mathSymbol == MathSymbol.plus ||
                expression.mathSymbol == MathSymbol.minus)
            {
                
            } else
            {
                //We are multiplying or dividing, therefore there are a group of eligable pieces for our expression.
                List<GameBoardPeice> eligablePieces = GetEligiblePiecesFromOperands(operand1, operand2, gameBoard);

                expression.Parse(eligablePieces, number);
            }
        }
    }

    //at this point we know that one or other is an number, therefor it won't error
    private int GetNumberFromOperands(EquationSymbol operand1, EquationSymbol operand2) 
    {
        EquationNumber num;
        num = (operand1 is EquationNumber) ? (EquationNumber)operand1 : (EquationNumber)operand2;

        return num.equationNumber;
    }

    private List<GameBoardPeice> GetEligiblePiecesFromOperands(EquationSymbol operand1, EquationSymbol operand2, GameBoard board)
    {
        EquationSymbolShapeCatagory shapeCatagoryExpression;
        List<GameBoardPeice> pieces;

        shapeCatagoryExpression = (operand1 is EquationSymbolShapeCatagory) ? (EquationSymbolShapeCatagory)operand1 : (EquationSymbolShapeCatagory)operand2;

        pieces = shapeCatagoryExpression.GetEligablePieces(board);

        return pieces;
    }
}

//Different equation types

public interface EquationSymbol { };

[System.Serializable]
public class EquationNumber : EquationSymbol
{
    public int equationNumber;

    public EquationNumber(int num) { equationNumber = num; }
}

public enum MathSymbol
{
    plus,
    minus,
    multiply,
    divide
}

public class EquationExpression : EquationSymbol
{
    public MathSymbol mathSymbol;

    public EquationExpression(MathSymbol symbol) { mathSymbol = symbol; }

    //Parse will be overloaded for each possible expression.
    public void Parse(List<GameBoardPeice> pieces, int number) //For Multiplying & Dividing shapes
    {
        switch(mathSymbol)
        {
            case MathSymbol.multiply:
                foreach(GameBoardPeice p in pieces)
                {
                    p.MultiplyPiece(number);
                }
                break;
        }
    }

    public void Parse(ShapeColor color, int number, GameBoard board) //For adding/deleting shapes of a color 
    {
        switch(mathSymbol)
        {
            case MathSymbol.plus:
                for(int i = 0; i < number; i++)
                {
                    board.AddRandomShapeFromColor(color);
                }             
                break;
        }
    }

    public void Parse(ShapeType type, int number, GameBoard board) //For adding/deleting shapes of a type 
    {
        switch (mathSymbol)
        {
            case MathSymbol.plus:
                for (int i = 0; i < number; i++)
                {
                    board.AddRandomColorFromShape(type);
                }
                break;
        }
    }
}

public abstract class EquationSymbolShapeCatagory : EquationSymbol
{
    //Color & Shape symbols will define how to get their own pieces
    public abstract List<GameBoardPeice> GetEligablePieces(GameBoard gameBoard);
};

public class EquationShapeType : EquationSymbolShapeCatagory
{
    public ShapeType shapeType;

    public EquationShapeType(ShapeType type) { shapeType = type; }

    public override List<GameBoardPeice> GetEligablePieces(GameBoard gameBoard)
    {
        return gameBoard.GetAllPiecesOfType(shapeType);
    }
}
