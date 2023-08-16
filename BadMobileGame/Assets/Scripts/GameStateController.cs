using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [Header("Public Variables")]
    public int score;

    public GameObject duplicationParticlePrefab;

    //Systems 
    public AbstractGameBoard gameBoard;
    public EquationParser equationParser;
    public CardCloud cloud;

    //Current phase
    private GameState gamePhase;

    //Pause
    public bool gamePaused;

    //Game Stats
    [SerializeField] private float difficultyFactor;

    private float shapeLossTimerMAX;
    private float shapeLossTimer;

    public UIController uiController;

    public void Start()
    {
        GameEntityController.duplicationEffectPrefab = duplicationParticlePrefab;
        InitState(GameState.game);
    }

    public void Update()
    {
        uiController.debugText.text = "phase = " + gamePhase.ToString() + ", total pieces = " + gameBoard.GetTotalPieces();

        switch(gamePhase)
        {
            case GameState.gameLoss:
                TickLossPhase();
                break;
        }
    }

    public void InitState(GameState phase)
    {
        gamePhase = phase;
        switch (phase)
        {
            case GameState.game:

                difficultyFactor = 0;
                shapeLossTimerMAX = 5f;
                shapeLossTimer = shapeLossTimerMAX;

                //Start by adding a few random pieces to the board to begin with
                for (int i = 0; i < 12; i++)
                {
                    gameBoard.AddRandomShapeToBoard();
                }

                //Generate staring deck
                cloud.DestroyAllCardsInDeck();
                cloud.GenerateCardBatch();

                break;
            case GameState.gameLoss:
                cloud.DestroyAllCardsInDeck();
                break;
        }
    }

    public void FixedUpdate()
    {
        switch (gamePhase)
        {
            case GameState.game:
                TickGamePhase();
                break;
        }
    }

    private void TickLossPhase()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitState(GameState.game);
        }
    }

    private void TickGamePhase()
    {
        if (gamePaused)
        {
            return;
        }

        //Tick timer
        if (shapeLossTimer > 0)
        {
            shapeLossTimer -= GetTimeDrainRate();
        } else
        {
            shapeLossTimer = shapeLossTimerMAX;
            RemoveShapesFromBoard(difficultyFactor);
        }

        //Increase difficulty
        difficultyFactor += Time.deltaTime;

        //Check for loss 
        if (gameBoard.GetTotalPieces() <= 0)
        {
            InitState(GameState.gameLoss);
        }
    }

    private void RemoveShapesFromBoard(float difficulty)
    {
        int numOfShapes = 1;

        numOfShapes = Mathf.RoundToInt(Mathf.Lerp(1, 5, Mathf.InverseLerp(1, 20, difficulty)));

        for (int i = 0; i < numOfShapes; i++)
        {
            gameBoard.RemoveRandomPiece();
        }
    }

    private float GetTimeDrainRate()
    {
        int pieces = gameBoard.GetTotalPieces();
        float multiplier = 1;

        if (pieces < 25)
        {
            multiplier = 0.8f;
        } else
        {
            multiplier = Mathf.Lerp(1, 30, Mathf.InverseLerp(1, 100, gameBoard.GetTotalPieces()));
        }
        
        return Time.deltaTime * multiplier;
    }
}

[System.Serializable]
public enum GameState
{
    titleScreen,
    game,
    gameLoss
}
