using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private bool isDragActive = false;
    private Vector2 screenPos;
    private Vector2 worldPos;
    private Draggable lastDragged;
    private bool clicked = false;
    // Update is called once per frame
    void Update()
    {
        //Dropping
        if (isDragActive && (Input.GetMouseButtonUp(0)) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            ExitDrag();
        }

        //Mouse
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPos = new Vector2(mousePos.x, mousePos.y);
            clicked = true;
        }
        //Touch Screen
        else if (Input.touchCount > 0) {
            screenPos = Input.GetTouch(0).position;
        }

        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        if (isDragActive)
        {
            StayDrag();
        }
        else if(clicked) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider != null) {
                    if (hit.collider.transform.TryGetComponent(out Draggable draggable)) {
                        EnterDrag(draggable);
                    }
                }         
            }
        }
    }

    void EnterDrag(Draggable drag) {
        lastDragged = drag;
        isDragActive = true;
        lastDragged.EnterDrag();
    }

    void StayDrag() {
        lastDragged.StayDrag(worldPos);
    }

    void ExitDrag() {
        isDragActive = false;
        lastDragged.ExitDrag();
        clicked = false;
    }
}
