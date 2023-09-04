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

    List<GameBoardPeice> GetAllSpecificPieces(ShapeType type, ShapeColor color);

    //Various methods for adding shapes to the board 
    void AddShapeToBoard(ShapeColor color, ShapeType type);

    void AddRandomShapeFromColor(ShapeColor color);

    void AddRandomColorFromShape(ShapeType type);

    void AddRandomShapeToBoard();

    //For making sure internal piece list is synced after multiplies shapes after multiplying or smthn
    void RegisterMultipleNewPeices(List<GameBoardPeice> newPieces);

    //Methods for removing pieces from a board
    void RemoveRandomPiece();

    void RemoveSpecificPieces(List<GameBoardPeice> pieces);

    void RemoveRandomShapeOfColor(ShapeColor color);

    void RemoveRandomColorOfShape(ShapeType type);

    void RemoveSpecificPiece(GameBoardPeice piece);

    void RemoveSpecificPiece(ShapeColor color, ShapeType type);
}
