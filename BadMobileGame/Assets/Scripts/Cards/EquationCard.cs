using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquationCard : Draggable
{
    CardCloud handManager; //reference to its cloud... could be useful later.
    EquationSymbol equationSymbol;
    DraggableUIGrid uiGrid;

    //(placeholder) Graphics...
    [SerializeField] TMP_Text label;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] BoxCollider2D coll;

    /// <summary>
    /// Sets values of this instance to given variables then initializes graphics.
    /// </summary>
    /// <param name="equationSymbol"></param>
    /// <param name="handManager"></param>
    public void Initialize(EquationSymbol equationSymbol, CardCloud handManager, DraggableUIGrid uiGrid) {
        this.equationSymbol = equationSymbol;
        this.handManager = handManager;
        this.uiGrid = uiGrid;
        DispelCardBackToCloud();
        InitGraphics();
    }

    public override void EnterDrag()
    {
        base.EnterDrag();
        coll.isTrigger = true;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        uiGrid.RemoveItemFromGrid(this);
    }

    /// <summary>
    /// Exits Drag with bonus detection for collision with EquationSlots.
    /// </summary>
    public override void ExitDrag()
    {
        //When dropping, check if it's over the EquationSlot object and slip it in
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, .2f);
        bool hitSlot = false;
        foreach(Collider2D coll in colls) {
            if (coll.CompareTag("EquationSlots") && coll.TryGetComponent(out EquationSlots slots)) {
                slots.SlotCard(this);
                hitSlot = true;
            }
        }

        //If this card wasn't dropped over EquationSlots then just send it back to the grid...
        if (!hitSlot) {
            uiGrid.AddItemToGrid(this);
        }
        coll.isTrigger = false;
        base.ExitDrag();
    }


    public EquationSymbol GetEquationSymbol() {
        return equationSymbol;
    }

    /// <summary>
    /// Initializes the graphics for this specific equationSymbol
    /// </summary>
    private void InitGraphics() {
        //Graphics functionality lives on each specific EquationSymbol
        equationSymbol.GraphicsSetup(sr, label);
    }

    /// <summary>
    /// 
    /// </summary>
    public void DispelCardBackToCloud() {
        uiGrid.AddItemToGrid(this);
    }

    public void DestroyCard() {
        handManager.RemoveCardFromDeck(this);
        //uiGrid.RemoveItemFromGrid(this);
        Destroy(gameObject);
    }

}
