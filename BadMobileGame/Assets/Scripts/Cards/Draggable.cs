using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Draggable : MonoBehaviour
{
    protected Vector3 pickedUpLocation;
    public Action OnExitDragComplete;


    public Vector3 GetPickedUpLoc() {
        return pickedUpLocation;
    }

    /// <summary>
    /// Callback when Dragging begins.
    /// </summary>
    public virtual void EnterDrag() {
        pickedUpLocation = transform.position;
    }

    /// <summary>
    /// Callback while in Drag... given position is the position of the desired drag.
    /// </summary>
    /// <param name="pos"></param>
    public virtual void StayDrag(Vector3 pos) {
        MoveTo(pos);
    }

    /// <summary>
    /// Moves object to position.
    /// </summary>
    /// <param name="pos"></param>
    public virtual void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// Callback for when Dragging ends.
    /// </summary>
    public virtual void ExitDrag() {
        OnExitDragComplete?.Invoke();
    }
}
