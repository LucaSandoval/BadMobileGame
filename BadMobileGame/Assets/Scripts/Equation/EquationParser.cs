using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquationParser : MonoBehaviour
{
    public AbstractGameBoard gameBoard;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ParseEquation(new EquationShapeType(ShapeType.triangle), 
                new AddExpression(), 
                new EquationColorType(ShapeColor.blue));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ParseEquation(new EquationColorType(ShapeColor.red),
                new AddExpression(),
                new EquationShapeType(ShapeType.square));
        }

        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                ParseEquation(new EquationColorType(ShapeColor.red),
                new AddExpression(),
                new EquationShapeType(ShapeType.square));
            }
        }

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ParseEquation(new EquationColorType(ShapeColor.blue),
        //        new MultiplyExpression(),
        //        new EquationNumber(2));
        //}
    }

    public void ParseEquation(EquationSymbol operand1, EquationExpression expression, EquationSymbol operand2)
    {
        //Failure cases
        //CASE 1: BOTH ARE THE SAME TYPE & NOT NUMBERS
        if (!(operand1 is EquationNumber) && !(operand2 is EquationNumber)) //both aren't numbers
        {
            if (operand1.GetType() == operand2.GetType()) { return; }
        }

        //Otherwise, let the expression type handle the results 
        expression.Parse(operand1, operand2, gameBoard);
    }
}

//Different equation types

public interface EquationSymbol {
    public void GraphicsSetup(SpriteRenderer sr, TMPro.TMP_Text label);
};

[System.Serializable]
public class EquationNumber : EquationSymbol
{
    public int equationNumber;

    public EquationNumber(int num) { equationNumber = num; }

    public void GraphicsSetup(SpriteRenderer sr, TMP_Text label)
    {
        label.text = equationNumber.ToString();
    }
}

//Different math implementations

public abstract class EquationExpression : EquationSymbol
{
    public abstract void Parse(EquationSymbol operand1, EquationSymbol operand2, GameBoard board);

    //Gets the number from the two operands
    protected int GetNumberFromOperands(EquationSymbol operand1, EquationSymbol operand2)
    {
        EquationNumber num;
        num = (operand1 is EquationNumber) ? (EquationNumber)operand1 : (EquationNumber)operand2;

        return num.equationNumber;
    }

    //Gets pieces list from two operands
    protected List<GameBoardPeice> GetOperatingPiecesListFromOperands(EquationSymbol operand1, EquationSymbol operand2, GameBoard board)
    {
        EquationSymbolShapeCatagory shapeCatagoryExpression;
        List<GameBoardPeice> pieces;

        shapeCatagoryExpression = (operand1 is EquationSymbolShapeCatagory) ? (EquationSymbolShapeCatagory)operand1 : (EquationSymbolShapeCatagory)operand2;

        pieces = shapeCatagoryExpression.GetAllPeicesOfSymbolType(board);

        return pieces;
    }

    //Gets the non-number equation symbol from two operands. (Only use if you know one is a number.)
    protected EquationSymbol GetNonNumberSymbolFromOperands(EquationSymbol operand1, EquationSymbol operand2)
    {
        return (operand1 is EquationNumber) ? operand2 : operand1;
    }

    public abstract void GraphicsSetup(SpriteRenderer sr, TMP_Text label);
}

public class MultiplyExpression : EquationExpression
{
    public override void Parse(EquationSymbol operand1, EquationSymbol operand2, GameBoard board)
    {
        //CASE 1: BOTH ARE NUMBERS
        if (operand1 is EquationNumber && operand2 is EquationNumber)
        {
            //Create a new card thats the result of the two nums multiplied 
            return;
        }

        //CASE 2: ONE OF THE TWO IS A NUMBER
        if (operand1 is EquationNumber || operand2 is EquationNumber)
        {
            int number = GetNumberFromOperands(operand1, operand2);
            List<GameBoardPeice> pieces = GetOperatingPiecesListFromOperands(operand1, operand2, board);

            foreach(GameBoardPeice p in pieces)
            {
                board.RegisterMultipleNewPeices(p.MultiplyPiece(number));
            }

            return;
        }

        //CASE 3: NEITHER ARE NUMBERS (At this point we know they aren't the same type of Symbol)
        EquationSymbolShapeCatagory shapeCatagory1 = (EquationSymbolShapeCatagory)operand1;
        EquationSymbolShapeCatagory shapeCatagory2 = (EquationSymbolShapeCatagory)operand2;

        //Currently arbitrary, but the left side operand will be the effected type of pieces,
        //while the right operand will be the type of thing to change them into. 
        shapeCatagory2.ChangePiecesToSymbolType(shapeCatagory1.GetAllPeicesOfSymbolType(board));
    }
    public override void GraphicsSetup(SpriteRenderer sr, TMP_Text label)
    {
        label.text = "*";
    }
}

public class AddExpression : EquationExpression
{
    public override void Parse(EquationSymbol operand1, EquationSymbol operand2, GameBoard board)
    {
        //CASE 1: BOTH ARE NUMBERS
        if (operand1 is EquationNumber && operand2 is EquationNumber)
        {
            //Create a new card thats the result of the two nums added 
            return;
        }

        //CASE 2: ONE OF THE TWO IS A NUMBER
        if (operand1 is EquationNumber || operand2 is EquationNumber)
        {
            int number = GetNumberFromOperands(operand1, operand2);

            EquationSymbolShapeCatagory shapeCatagory = (EquationSymbolShapeCatagory)GetNonNumberSymbolFromOperands(operand1, operand2);
            shapeCatagory.AddPiecesOfSymbolTypeToBoard(number, board);

            return;
        }

        //CASE 3: NEITHER ARE NUMBERS (At this point we know they aren't the same type of Symbol)
        EquationSymbolShapeCatagory shapeCatagory1 = (EquationSymbolShapeCatagory)operand1;
        EquationSymbolShapeCatagory shapeCatagory2 = (EquationSymbolShapeCatagory)operand2;

        shapeCatagory1.CombineWithOtherShapeCatagory(shapeCatagory2, board);
    }
    public override void GraphicsSetup(SpriteRenderer sr, TMP_Text label)
    {
        label.text = "+";
    }
}

///////////

public abstract class EquationSymbolShapeCatagory : EquationSymbol
{
    //Color & Shape symbols will define how to get their own pieces
    public abstract List<GameBoardPeice> GetAllPeicesOfSymbolType(GameBoard gameBoard);

    //Takes in a list of pieces and converts them all to this Equation Symbol's type and/or color
    public abstract void ChangePiecesToSymbolType(List<GameBoardPeice> pieces);

    //Adds a given number of pieces to a given board based on this symbol type 
    public abstract void AddPiecesOfSymbolTypeToBoard(int number, GameBoard gameBoard);

    //Each symbol type should individually figure out how to combine itself with another catagory
    public abstract void CombineWithOtherShapeCatagory(EquationSymbolShapeCatagory other, GameBoard board);
    public abstract void GraphicsSetup(SpriteRenderer sr, TMP_Text label);
}
public class EquationShapeType : EquationSymbolShapeCatagory
{
    public ShapeType shapeType;

    public EquationShapeType(ShapeType type) { shapeType = type; }

    public override void AddPiecesOfSymbolTypeToBoard(int number, GameBoard gameBoard)
    {
        for(int i = 0; i < number; i++)
        {
            gameBoard.AddRandomColorFromShape(shapeType);
        }
    }

    public override void ChangePiecesToSymbolType(List<GameBoardPeice> pieces)
    {
        foreach(GameBoardPeice p in pieces)
        {
            BasicShape shape = (BasicShape)p;
            shape.ChangeShapeType(shapeType);
        }
    }

    public override void CombineWithOtherShapeCatagory(EquationSymbolShapeCatagory other, GameBoard board)
    {
        if (other is EquationColorType)
        {
            EquationColorType colorType = (EquationColorType)other;
            board.AddShapeToBoard(colorType.shapeColor, shapeType);
        }
    }

    public override List<GameBoardPeice> GetAllPeicesOfSymbolType(GameBoard gameBoard)
    {
        return gameBoard.GetAllPiecesOfType(shapeType);
    }

    public override void GraphicsSetup(SpriteRenderer sr, TMP_Text label)
    {
        sr.sprite = ShapeUtil.ShapeTypeToSprite(shapeType);
    }
}

public class EquationColorType : EquationSymbolShapeCatagory
{
    public ShapeColor shapeColor;

    public EquationColorType(ShapeColor color) { shapeColor = color; }

    public override void AddPiecesOfSymbolTypeToBoard(int number, GameBoard gameBoard)
    {
        for (int i = 0; i < number; i++)
        {
            gameBoard.AddRandomShapeFromColor(shapeColor);
        }
    }

    public override void ChangePiecesToSymbolType(List<GameBoardPeice> pieces)
    {
        foreach (GameBoardPeice p in pieces)
        {
            BasicShape shape = (BasicShape)p;
            shape.ChangeShapeColor(shapeColor);
        }
    }

    public override void CombineWithOtherShapeCatagory(EquationSymbolShapeCatagory other, GameBoard board)
    {
        if (other is EquationShapeType)
        {
            EquationShapeType shapeType = (EquationShapeType)other;
            board.AddShapeToBoard(shapeColor, shapeType.shapeType);
        }
    }

    public override List<GameBoardPeice> GetAllPeicesOfSymbolType(GameBoard gameBoard)
    {
        return gameBoard.GetAllPiecesOfColor(shapeColor);
    }

    public override void GraphicsSetup(SpriteRenderer sr, TMP_Text label)
    {
        sr.color = ShapeUtil.ShapeColorToColor(shapeColor);
    }
}
