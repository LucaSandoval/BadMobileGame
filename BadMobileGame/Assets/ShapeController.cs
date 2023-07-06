using UnityEngine;

public class ShapeController : MonoBehaviour
{
    public Animator circAnimator;  // Reference to the Animator component

    public int shapeNumber;  // Number representing the desired shape

    private static readonly int ShapeParameter = Animator.StringToHash("Shape");

    private void Start()
    {
        if (circAnimator == null)
        {
            Debug.LogError("CircAnimator reference not set. Please assign the CircController object to the script.");
            enabled = false;
        } else {
            SetShapeParameter(shapeNumber);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Example key to trigger shape change, you can change it as per your requirement
        {
            SetShapeParameter(shapeNumber);
        }
    }

    private void SetShapeParameter(int shape)
    {
        if (circAnimator != null)
        {
            circAnimator.SetInteger(ShapeParameter, shape);
        }
    }
}
