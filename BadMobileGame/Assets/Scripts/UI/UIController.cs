using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Misc.")]
    public Text debugText;

    public Text scoreText;

    public Slider goalTimeSlider;

    public Text goalCompleteText;
    private float goalCompleteAlpha;


    public void ShowGoalComplete(float time)
    {
        goalCompleteAlpha = Mathf.Clamp(goalCompleteAlpha += time, 0, 1);
    }

    private void FixedUpdate()
    {
        if (goalCompleteAlpha > 0) { goalCompleteAlpha -= Time.deltaTime; }  
    }

    private void Update()
    {
        if (goalCompleteText != null)
        {
            goalCompleteText.gameObject.SetActive(goalCompleteAlpha > 0);

            goalCompleteText.text = "GOAL COMPLETE";
            goalCompleteText.color = new Color(1, 1, 1, goalCompleteAlpha);
        }
    }
}
