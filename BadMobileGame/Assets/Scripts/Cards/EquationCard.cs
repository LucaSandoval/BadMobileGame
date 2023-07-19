using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquationCard : Draggable
{
    CardCloud cloud; //reference to its cloud... could be useful later.
    EquationSymbol equationSymbol;

    //(placeholder) Graphics...
    [SerializeField] TMP_Text label;
    [SerializeField] SpriteRenderer sr;

    /// <summary>
    /// Sets values of this instance to given variables then initializes graphics.
    /// </summary>
    /// <param name="equationSymbol"></param>
    /// <param name="cloud"></param>
    public void Initialize(EquationSymbol equationSymbol, CardCloud cloud) {
        this.equationSymbol = equationSymbol;
        this.cloud = cloud;
        InitGraphics();
    }

    /// <summary>
    /// Exits Drag with bonus detection for collision with EquationSlots.
    /// </summary>
    public override void ExitDrag()
    {
        //When dropping, check if it's over the EquationSlot object and slip it in
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, .2f);
        foreach(Collider2D coll in colls) {
            if (coll.CompareTag("EquationSlots") && coll.TryGetComponent(out EquationSlots slots)) {
                slots.SlotCard(this);
            }
        }
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
}
