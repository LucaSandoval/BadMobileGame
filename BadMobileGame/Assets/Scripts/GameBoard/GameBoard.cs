using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Any game board will have a collection of GameBoardPeices that can be 
//manipulated and referenced with the following functions.
[SerializeField]
public interface GameBoard
{
    //General functions for tracking the state of the board
    int GetTotalPieces();

    //Different sorting functions to get pieces of 
    List<GameBoardPeice> GetAllPieces();

    List<GameBoardPeice> GetAllPiecesOfType(ShapeType type);

    List<GameBoardPeice> GetAllPiecesOfColor(ShapeColor color);

    //Various methods for adding shapes to the board 
    void AddShapeToBoard(ShapeColor color, ShapeType type);

    void AddRandomShapeFromColor(ShapeColor color);

    void AddRandomColorFromShape(ShapeType type);
}
