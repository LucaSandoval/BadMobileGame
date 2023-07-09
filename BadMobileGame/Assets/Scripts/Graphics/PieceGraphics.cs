using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceGraphics : MonoBehaviour
{
    private float stretch = 1.25f;
    private Action OnLerpComplete;

    Vector3 target = Vector3.one;
    float strength = 6.5f;

    public void Stretch() {
        target = new Vector3(stretch, stretch, stretch);
        strength = 10f;

        OnLerpComplete += ReturnToNormal;
    }


    private void Update()
    {
        DoLerp();
    }

    private void DoLerp()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, target, strength * Time.deltaTime);

        //If we have reached the target...
        if ((transform.localScale - target).magnitude < .01f) {
            OnLerpComplete?.Invoke();
        }

    }

    private void ReturnToNormal() {
        target = Vector3.one;
        strength = 6.5f;
        OnLerpComplete -= ReturnToNormal;
    }
}
