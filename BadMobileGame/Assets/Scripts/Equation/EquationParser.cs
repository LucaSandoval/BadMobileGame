using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationParser : MonoBehaviour
{
    public AbstractGameBoard gameBoard;

    public void ParseEquation(EquationSymbol operand1, EquationExpression expression, EquationSymbol operand2)
    {
        switch(expression.mathSymbol)
        {
            case MathSymbol.plus:
                break;
        }
    }
}

//Different equation types

public interface EquationSymbol {};

[System.Serializable]
public struct EquationNumber : EquationSymbol
{
    public int equationNumber;
}

public enum MathSymbol
{
    plus,
    minus,
    multiply,
    divide
}

public struct EquationExpression : EquationSymbol
{
    public MathSymbol mathSymbol;
}

public struct EquationShapeType : EquationSymbol
{
    public ShapeType shapeType;
}
