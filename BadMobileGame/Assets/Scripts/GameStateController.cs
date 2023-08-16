using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{

    public GameObject duplicationParticlePrefab;

    //Systems 
    public AbstractGameBoard gameBoard;
    public EquationParser equationParser;
    public CardCloud cloud;

    //Current phase
    private GamePhase gamePhase;

    //Pause
    public bool gamePaused;

    //Game Stats
    private float difficultyFactor;

    private float shapeLossTimerMAX;
    private float shapeLossTimer;

    public void Start()
    {
        GameEntityController.duplicationEffectPrefab = duplicationParticlePrefab;
        gamePhase = GamePhase.game;
        InitPhase(gamePhase);
    }

    public void InitPhase(GamePhase phase)
    {
        switch (phase)
        {
            case GamePhase.game:

                difficultyFactor = 0;
                shapeLossTimerMAX = 5f;
                shapeLossTimer = shapeLossTimerMAX;

                //Start by adding a few random pieces to the board to begin with
                for (int i = 0; i < 12; i++)
                {
                    gameBoard.AddRandomShapeToBoard();
                }
                //Generate staring deck
                cloud.GenerateCardBatch();

                break;
        }
    }

    public void FixedUpdate()
    {
        switch (gamePhase)
        {
            case GamePhase.game:
                TickGamePhase();
                break;
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
            shapeLossTimer -= Time.deltaTime;
        } else
        {
            shapeLossTimer = shapeLossTimerMAX;
            //RemoveShapesFromBoard(difficultyFactor);
        }

        //Increase difficulty
        difficultyFactor += Time.deltaTime;
    }

    private void RemoveShapesFromBoard(float difficulty)
    {
        int numOfShapes = 1;

        for(int i = 0; i < numOfShapes; i++)
        {
            gameBoard.RemoveRandomPiece();
        }
    }
}

public enum GamePhase
{
    titleScreen,
    game
}
