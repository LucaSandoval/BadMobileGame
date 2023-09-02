using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling INSTANCE;

    [SerializeField] private int initialPoolSize = 50;

    private Queue<BasicShape> objectPool = new Queue<BasicShape>();

    // Start is called before the first frame update
    void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Get initial pool ready to avoid a big burst of Instantiates during gameplay.
        for (int i = 0; i < initialPoolSize; i++)
        {
            //Arbitrarily chosen Type and Color
            BasicShape basicShape = SpawnBasicShape();
            basicShape.gameObject.SetActive(false);
            objectPool.Enqueue(basicShape);
        }
    }

    private static BasicShape SpawnBasicShape()
    {
        GameObject newPiece = new GameObject();
        BasicShape shapeClass = newPiece.AddComponent<BasicShape>();

        return shapeClass;
    }

    public BasicShape PooledSpawnBasicShape(ShapeType type, ShapeColor color)
    {
        if (objectPool.Count == 0)
        {
            BasicShape basicShape = SpawnBasicShape();

            basicShape.gameObject.SetActive(false);
            objectPool.Enqueue(basicShape);
        }


        BasicShape pooledShape = objectPool.Dequeue();
        Rigidbody rb = pooledShape.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        pooledShape.gameObject.SetActive(true);
        pooledShape.Initialize(type, color);

        return pooledShape;
    }

    public void PooledBasicShapeDestroy(BasicShape shape)
    {
        shape.gameObject.SetActive(false);
        objectPool.Enqueue(shape);
    }
}
