using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    //Systems 
    public AbstractGameBoard gameBoard;
    public EquationParser equationParser;
    public CardCloud cloud;

    //Current phase
    private GamePhase gamePhase;

    //Pause
    public bool gamePaused;

    public void Start()
    {
        gamePhase = GamePhase.game;
        InitPhase(gamePhase);
    }

    public void InitPhase(GamePhase phase)
    {
        switch (phase)
        {
            case GamePhase.game:
                //Start by adding a few random pieces to the board to begin with
                for(int i = 0; i < 12; i++)
                {
                    gameBoard.AddRandomShapeToBoard();
                }

                cloud.GenerateCardBatch();

                break;
        }
    }

    public void Update()
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
    }
}

public enum GamePhase
{
    titleScreen,
    game
}
