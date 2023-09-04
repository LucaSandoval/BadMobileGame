using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private float goalTimerMax;
    public float goalTimer;

    private bool ObjectiveActive;

    [SerializeField] public List<ShapeGoal> CurrentGoals;

    public GameObject GoalParent;
    private GameObject PieceGoalPrefab;

    public AbstractGameBoard gameBoard;

    public UIController uiController;
 

    void Awake()
    {
        CurrentGoals = new List<ShapeGoal>();
        PieceGoalPrefab = Resources.Load<GameObject>("Goals/goal_prefab");
    }

    void Start()
    {
        //ObjectiveActive = false;
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    BeginNewObjective(10);
        //}
    }

    public bool ObjectiveCompleted()
    {
        return (CurrentGoals.Count == 0 && goalTimer > 0 && ObjectiveActive);
    }

    public bool ObjectiveFailed()
    {
        return (CurrentGoals.Count > 0 && goalTimer <= 0 && ObjectiveActive);
    }

    public void StopObjective()
    {
        ObjectiveActive = false;
        ClearGoals();
    }

    private void FixedUpdate()
    {
        if (ObjectiveActive)
        {
            if (goalTimer > 0)
            {
                goalTimer -= Time.deltaTime;
            }

            if (uiController != null)
            {
                uiController.goalTimeSlider.maxValue = goalTimerMax;
                uiController.goalTimeSlider.value = goalTimer;
            }
        }
    }

    public void BeginNewObjective(float difficulty)
    {
        if (CurrentGoals == null)
        {
            CurrentGoals = new List<ShapeGoal>();
        }

        goalTimerMax = Mathf.RoundToInt(Mathf.Lerp(30, 5, Mathf.InverseLerp(1, GameStateController.MaximumDifficultyValue, difficulty)));
        goalTimer = goalTimerMax;

        CreateNewGoalSet(difficulty);
        ObjectiveActive = true;
    }

    public void CreateNewGoalSet(float difficulty)
    {
        ClearGoals();

        //Pick an ammount to generate based on the difficulty 
        int numOfGoals = 1;
        numOfGoals = Mathf.RoundToInt(Mathf.Lerp(1, 5, Mathf.InverseLerp(1, GameStateController.MaximumDifficultyValue, difficulty)));

        //Debug.Log(numOfGoals);

        for (int i = 0; i < numOfGoals; i++)
        {
            //WIP: Pick a random color and type for the shape as well as a random ammount
            ShapeColor randColor = (ShapeColor)Random.Range(0, System.Enum.GetValues(typeof(ShapeColor)).Length);
            ShapeType randType = (ShapeType)Random.Range(0, System.Enum.GetValues(typeof(ShapeType)).Length);
            
            CreateNewShapeGoal(Random.Range(5, 10), randType, randColor);
        }
    }

    public void CreateNewShapeGoal(int max, ShapeType type, ShapeColor color)
    {
        if (gameBoard == null) { return; }

        //Create prefab
        GameObject NewGoalPrefab = Instantiate(PieceGoalPrefab);

        //Creat goal class
        ShapeGoal NewGoal = NewGoalPrefab.AddComponent<ShapeGoal>();
        NewGoal.Initialize(max, gameBoard, type, color, this);

        //Setup visual element
        NewGoalPrefab.GetComponent<PiecesGoalPrefab>().goal = NewGoal;

        //Add to list on screen
        NewGoalPrefab.transform.SetParent(GoalParent.transform);

        CurrentGoals.Add(NewGoal);
    } 

    public void ClearGoals()
    {
        //Clear previous goals
        if (CurrentGoals.Count > 0)
        {
            foreach(ShapeGoal goal in CurrentGoals)
            {
                Destroy(goal.gameObject);
            }
            CurrentGoals.Clear();
        }
    }
}
