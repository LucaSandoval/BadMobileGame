using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShape : AbstractPiece
{
    //Types
    protected ShapeType shapeType;
    protected ShapeColor shapeColor;

    //Sprites (WIP, probably change this later)
    private Sprite squareSprite;
    private Sprite triangleSprite;
    private Sprite circleSprite;

    public void Initialize(ShapeType type, ShapeColor color)
    {
        base.BaseInitialize();
        shapeType = type;
        shapeColor = color;

        squareSprite = Resources.Load<Sprite>("Sprites/base_shapes1");
        triangleSprite = Resources.Load<Sprite>("Sprites/base_shapes2");
        circleSprite = Resources.Load<Sprite>("Sprites/base_shapes3");

        gameObject.name = shapeColor.ToString() + " " + shapeType.ToString();
    }

    public override GameBoardPeice DuplicatePiece()
    {
        BasicShape newShape = GameEntityController.SpawnShape(shapeType, shapeColor);
        return newShape;
    }

    public override Sprite GetBaseShapeSprite()
    {
        switch(shapeType)
        {
            case ShapeType.square:
                return squareSprite;
            case ShapeType.triangle:
                return triangleSprite;
            case ShapeType.circle:
                return circleSprite;
        }

        return null;
    }

    public override Color GetBaseColor()
    {
        switch(shapeColor)
        {
            case ShapeColor.red:
                return Color.red;
            case ShapeColor.green:
                return Color.green;
            case ShapeColor.blue:
                return Color.blue;
        }

        return Color.white;
    }

    public ShapeType GetShapeType()
    {
        return shapeType;
    }

    public ShapeColor GetShapeColor()
    {
        return shapeColor;
    }

    public void ChangeShapeType(ShapeType newType)
    {
        shapeType = newType;
    }

    public void ChangeShapeColor(ShapeColor newColor)
    {
        shapeColor = newColor;
    }
}


[System.Serializable]
public enum ShapeType
{
    square,
    circle,
    triangle
}

[System.Serializable]
public enum ShapeColor
{
    red,
    blue,
    green
}
