using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShape : MonoBehaviour, IShape
{
    //This constructor will define how basic are spawned.
    public BaseShape(float xPos, float yPos)
    {
        gameObject.transform.position = new Vector2(xPos, yPos);
    }

    public void DestroyShape()
    {
        Destroy(gameObject);
    }

    public IShape[] MuliplyShape(int factor)
    {
        return null;
    }
}
