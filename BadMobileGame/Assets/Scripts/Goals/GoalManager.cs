using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public List<ShapeGoal> CurrentGoals;

    public GameObject GoalParent;
    private GameObject PieceGoalPrefab;

    public AbstractGameBoard gameBoard;

    void Start()
    {
        CurrentGoals = new List<ShapeGoal>();
        PieceGoalPrefab = Resources.Load<GameObject>("Goals/goal_prefab");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewShapeGoal(5, ShapeType.circle, ShapeColor.blue);
        }   
    }

    public void CreateNewShapeGoal(int max, ShapeType type, ShapeColor color)
    {
        if (gameBoard == null) { return; }

        //Create prefab
        GameObject NewGoalPrefab = Instantiate(PieceGoalPrefab);

        //Creat goal class
        ShapeGoal NewGoal = NewGoalPrefab.AddComponent<ShapeGoal>();
        NewGoal.Initialize(max, gameBoard, type, color);

        //Setup visual element
        NewGoalPrefab.GetComponent<PiecesGoalPrefab>().goal = NewGoal;

        //Add to list on screen
        NewGoalPrefab.transform.SetParent(GoalParent.transform);
    } 
}
