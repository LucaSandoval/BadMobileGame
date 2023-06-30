using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public List<GameBoardPeice> shapes;

    void Start()
    {
        GameBoardPeice test1 = GameEntityController.SpawnShape(ShapeType.square, ShapeColor.red);
        test1.SetPosition(new Vector2(-2f, 0));
        test1.MultiplyPiece(3);

        GameBoardPeice test2 = GameEntityController.SpawnShape(ShapeType.triangle, ShapeColor.red);
        test2.SetPosition(new Vector2(2f, 0));
        test2.MultiplyPiece(3);
    }
}
