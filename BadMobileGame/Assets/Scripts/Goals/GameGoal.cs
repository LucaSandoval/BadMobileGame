using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Some kind of Objective in the game. Might just be 'get this many shapes' but
//using an interface will give us flexibility of we want other goals/objectives 
//(maybe destroy x pieces idk) 
public interface GameGoal
{
    bool IsComplete();

    void CompleteGoal();
}
