using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationSlots : MonoBehaviour
{
    private bool DEBUG = false;
    EquationCard left;
    EquationCard middle;
    EquationCard right;
    bool didOnce = false;

    [SerializeField] Transform leftSlot;
    [SerializeField] Transform middleSlot;
    [SerializeField] Transform rightSlot;

    [SerializeField] EquationParser equationParser;

    public void SlotCard(EquationCard card) {
        if(DEBUG) print("card's Equation Symbol: " + card.GetEquationSymbol());
        switch (card.GetEquationSymbol()) {
            case EquationExpression _: //expressions only go in the middle.
                if(DEBUG) print("Expression");
                middle = ReassignCard(middle, card, middleSlot);
                break;
            default: //all else are able to be left OR right... depending on proximity.
                if (DEBUG) print("NOT Expression");
                AssignToClosest(card);
                break;
        }
    }


    private void Update()
    {
        if (left != null && middle != null && right != null && !didOnce) {
            RunEquation();
            didOnce = true;
        }
    }

    /// <summary>
    /// Assigns the given card to the closer slot (left or right)... 
    /// </summary>
    /// <param name="card"></param>
    private void AssignToClosest(EquationCard card) {
        float leftMag = (leftSlot.position - card.transform.position).magnitude;
        float rightMag = (rightSlot.position - card.transform.position).magnitude;

        bool leftCloser = leftMag < rightMag;

        if (leftCloser)
        {
            left = ReassignCard(left, card, leftSlot);
        }
        else {
            right = ReassignCard(right, card, rightSlot);
        }
    }

    private EquationCard ReassignCard(EquationCard oldCard, EquationCard newCard, Transform slot) {
        oldCard?.MoveTo(newCard.GetPickedUpLoc()); //move oldCard to where this newCard came from... swap position.

        //checks if this newCard is actually already assigned a value. Swaps values if so.
        if (newCard.GetPickedUpLoc() == leftSlot.position && newCard == left) left = oldCard;
        if (newCard.GetPickedUpLoc() == rightSlot.position && newCard == right) right = oldCard;

        newCard.MoveTo(slot.position);  //move newCard to the designated slot location.
        newCard.OnExitDragComplete += OnCardDragComplete; //subscribes to listen for when this card moves next.
        return newCard;
        
    }

    /// <summary>
    /// Rechecks all its card slots to check if card was moved off and should be removed or not.
    /// </summary>
    private void OnCardDragComplete() {
        //if the card's new position after this drag is different than what is expected. It loses its value status.
        if (middle != null && middleSlot.transform.position != middle.transform.position) middle = null;
        if (left != null && leftSlot.transform.position != left.transform.position) left = null;
        if (right != null && rightSlot.transform.position != right.transform.position) right = null;

    }

    public void RunEquation() {
        //hookup to EquationParser here...
        equationParser.ParseEquation(left.GetEquationSymbol(), (EquationExpression)(middle.GetEquationSymbol()), right.GetEquationSymbol());

        //debug
        left.DestroyCard();
        middle.DestroyCard();
        right.DestroyCard();
    }
}
