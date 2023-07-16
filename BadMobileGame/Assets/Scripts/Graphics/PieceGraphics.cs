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

    public void Stretch(Vector2 velocity) {
        float magnitude = velocity.magnitude;
        float maxOutStretch = 5f;
        float magStretch = 1f + ((stretch - 1f) * Mathf.Clamp(magnitude / maxOutStretch, 0f, 1f));
        target = new Vector3(magStretch, magStretch, magStretch);
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
