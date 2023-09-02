using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEntityController
{
    public static GameObject duplicationEffectPrefab;

    public static BasicShape SpawnShape(ShapeType type, ShapeColor color)
    {
   /*     GameObject newPiece = new GameObject();
        BasicShape shapeClass = newPiece.AddComponent<BasicShape>();
        shapeClass.Initialize(type, color);*/

        return ObjectPooling.INSTANCE.PooledSpawnBasicShape(type, color);
    }
}
