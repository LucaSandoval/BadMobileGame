using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiecesGoalPrefab : MonoBehaviour
{
    public ShapeGoal goal;

    public Text piecesText;
    public Image pieceIcon;

    public void Update()
    {
        if (goal != null)
        {
            piecesText.text = goal.GetCurrentProgress().ToString() + " / " + goal.GetMaxProgress().ToString();

            pieceIcon.sprite = ShapeUtil.ShapeTypeToSprite(goal.GetShapeType());
            pieceIcon.color = ShapeUtil.ShapeColorToColor(goal.GetShapeColor());
        }
    }
}
