using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPiece : MonoBehaviour, GameBoardPeice
{
    //Visuals
    protected SpriteRenderer ren;
    protected Sprite baseSprite;

    
    //Sets up basic components and default info
    public virtual void BaseInitialize()
    {
        ren = gameObject.AddComponent<SpriteRenderer>();

        //Fallback sprite if nothing else loads for some reason
        baseSprite = Resources.Load<Sprite>("Sprites/base_shapes1");
    }

    //Should be overwritten
    public virtual void DestroyPiece()
    {
        Destroy(gameObject);
    }

    //Should be overwritten
    public virtual List<GameBoardPeice> MultiplyPiece(int factor)
    {
        Vector2 originPoint = transform.position;
        float spawnRadius = 1f;

        //Create a new array of output peices
        //Set this to be the first piece and position it at the correct
        //spawn point.
        List<GameBoardPeice> newPeices = new List<GameBoardPeice>();
        newPeices.Add(this);
        newPeices[0].SetPosition(CalculatePointOnCircle(spawnRadius, 0, originPoint));

        float step = 360 / factor;

        //For the ammount of times this is multiplied, spawn a new shape and
        //position it correctly.
        for (int i = 1; i < factor; i++)
        {
            GameBoardPeice newShape = DuplicatePiece();
            newShape.SetPosition(CalculatePointOnCircle(spawnRadius, i * step, originPoint));
            newPeices.Add(newShape);
        }

        return newPeices;
    }

    public abstract GameBoardPeice DuplicatePiece();

    protected Vector2 CalculatePointOnCircle(float radius, float angleDegrees, Vector2 center)
    {
        // Convert angle from degrees to radians
        float angleRadians = Mathf.Deg2Rad * angleDegrees;

        // Calculate the point coordinates on the circle
        float x = center.x + radius * Mathf.Cos(angleRadians);
        float y = center.y + radius * Mathf.Sin(angleRadians);

        return new Vector2(x, y);
    }

    public virtual Sprite GetBaseShapeSprite()
    {
        return baseSprite;
    }

    //Should be overwritten
    protected virtual void Update()
    {
        //Set shape visuals every frame
        if (ren && GetBaseShapeSprite())
        {
            ren.sprite = GetBaseShapeSprite();
        }
    }

    public void SetPosition(Vector2 position)
    {
        gameObject.transform.position = position;
    }
}
