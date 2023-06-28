using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceController : MonoBehaviour
{
    public BasicShape SpawnShape(ShapeType type, ShapeColor color)
    {
        GameObject newPiece = new GameObject();
        BasicShape shapeClass = newPiece.AddComponent<BasicShape>();
        shapeClass.Initialize(type, color, this);

        return shapeClass;
    }
}
