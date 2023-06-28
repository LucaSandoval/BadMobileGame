using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Any peice should have the following properties. 
//The abstract Shape class will define Shape base functionality
//(Which I can't imagine will be much different between shapes but
//you never know idk. We have this so other peices could be supported:
//like special power ups or bombs or whatever)
public interface GameBoardPeice 
{
    //Muliply this shape by a factor of whatever. Returns an array with references to the new shapes. 
    GameBoardPeice[] MultiplyPiece(int factor);

    //Destroys this shape. Children can define how that looks visually. 
    void DestroyPiece();

    //Sets the position of this peice (no physics involved) 
    void SetPosition(Vector2 position);

    //Returns an exact copy of this piece 
    GameBoardPeice DuplicatePiece();
}
