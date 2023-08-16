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



        gameObject.name = shapeColor.ToString() + " " + shapeType.ToString();
    }

    public override GameBoardPeice DuplicatePiece()
    {
        PlayDuplicationEffect(); // Play the effect when duplicating

        BasicShape newShape = GameEntityController.SpawnShape(shapeType, shapeColor);
        return newShape;
    }


    public override Sprite GetBaseShapeSprite()
    {
        return ShapeUtil.ShapeTypeToSprite(shapeType);
    }

    public override Color GetBaseColor()
    {
        return ShapeUtil.ShapeColorToColor(shapeColor);
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

    private void PlayDuplicationEffect()
    {
        if (GameEntityController.duplicationEffectPrefab)
        {
            GameObject effectInstance = Instantiate(GameEntityController.duplicationEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = ShapeUtil.ShapeColorToColor(shapeColor);
            Destroy(effectInstance, 5f); // Assumes the effect duration is 5 seconds
        }
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


public static class ShapeUtil {

    static Sprite squareSprite = Resources.Load<Sprite>("Sprites/base_shapes1");
    static Sprite triangleSprite = Resources.Load<Sprite>("Sprites/base_shapes2");
    static Sprite circleSprite = Resources.Load<Sprite>("Sprites/base_shapes3");

    public static Color ShapeColorToColor(ShapeColor sc) {
        switch (sc) {
            case ShapeColor.blue:
                return Color.blue;
            case ShapeColor.green:
                return Color.green;
            case ShapeColor.red:
                return Color.red;
            default:
                return Color.white;
        }
    }

    public static Sprite ShapeTypeToSprite(ShapeType shape) {
        switch (shape) {
            case ShapeType.square:
                return squareSprite;
            case ShapeType.circle:
                return circleSprite;
            case ShapeType.triangle:
                return triangleSprite;
            default:
                return null;
        }
    }
}