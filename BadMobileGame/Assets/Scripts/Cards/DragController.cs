using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private bool isDragActive = false;
    private Vector2 screenPos;
    private Vector2 worldPos;
    private Draggable lastDragged;

    // Update is called once per frame
    void Update()
    {
        //Dropping
        if (isDragActive && (Input.GetMouseButtonUp(0)) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            //print("Exiting drag");
            ExitDrag();
        }

        //Mouse
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPos = new Vector2(mousePos.x, mousePos.y);
        }
        //Touch Screen
        else if (Input.touchCount > 0) {
            screenPos = Input.GetTouch(0).position;
        }

        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        if (isDragActive)
        {
            Drag();
        }
        else {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider.transform.TryGetComponent(out Draggable draggable)) {
                    lastDragged = draggable;
                    EnterDrag();
                }
            }
        }
    }

    void EnterDrag() {
        isDragActive = true;
    }

    void Drag() {
        lastDragged.MoveTo(worldPos);
    }

    void ExitDrag() {
        isDragActive = false;
    }
}