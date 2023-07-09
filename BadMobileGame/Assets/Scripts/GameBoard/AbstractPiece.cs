using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPiece : MonoBehaviour, GameBoardPeice
{
    //Visuals
    protected GameObject graphicsParent;
    protected PieceGraphics pieceGraphics;
    protected SpriteRenderer ren;
    protected Sprite baseSprite;

    //Physics
    protected RigidBodyStats rigidBodyStats;
    protected Rigidbody2D rb;

    
    //Sets up basic components and default info
    public virtual void BaseInitialize()
    {
        //Set up graphics
        graphicsParent = new GameObject();
        pieceGraphics = graphicsParent.AddComponent<PieceGraphics>();
        graphicsParent.transform.parent = transform;
        graphicsParent.name = "Graphics Parent";
        ren = graphicsParent.AddComponent<SpriteRenderer>();

        //Setting tag of Shape to "Shape"
        gameObject.tag = "Shape";

        //Set up Phyrics
        InitiializeRigidBody();
        InitializeBaseShapeCollider();

        //... prevents weird physics stacking...
        Vector3 dir = Random.insideUnitCircle;
        float strength = 1f;
        rb.AddForce(dir.normalized * strength);
        

        //Fallback sprite if nothing else loads for some reason
        baseSprite = Resources.Load<Sprite>("Sprites/base_shapes1");
    }

    private void InitiializeRigidBody() {
        //Add and configure Rigidbody2D
        rb = gameObject.AddComponent<Rigidbody2D>();
        rigidBodyStats = Resources.Load<RigidBodyStats>("ScriptableObjects/BaseStats");
        rb.mass = rigidBodyStats.mass;
        rb.gravityScale = rigidBodyStats.gravityScale;
        rb.drag = rigidBodyStats.linearDrag;
        rb.angularDrag = rigidBodyStats.angularDrag;
        rb.sharedMaterial = rigidBodyStats.physicsMaterial;

        //prevent weird collisions
        float radius = .3f;
        Collider2D[] colls = Physics2D.OverlapCircleAll(rb.position, radius);
        foreach(Collider2D coll in colls) {
            if (coll.CompareTag("Shape")) {
                if (coll.TryGetComponent(out Rigidbody2D otherRb)) {
                    Vector2 opposite = rb.transform.position - otherRb.transform.position;
                    float strength = 10f;
                    otherRb.AddForce(opposite.normalized * strength, ForceMode2D.Impulse);

                }
            }
        }
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

    //Sets the base collider as a circle. CAN BE OVERRIDDEN.
    public virtual void InitializeBaseShapeCollider() 
    {
        gameObject.AddComponent<CircleCollider2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pieceGraphics.Stretch();
    }

}
