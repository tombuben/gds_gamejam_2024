using UnityEngine;
using UnityEngine.Splines;

public class TrpaslikCircleRunner : MonoBehaviour
{
    public bool isRunning = true;
    public SplineContainer movementSpline;
    public float movementSpeed = 3.0f;
    public float splinePosition;
    public DialogInstance dialogInstance;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogInstance.DialogOpened += DialogOpened;
    }

    private void DialogOpened()
    {
        movementSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementSpeed > 0)
        {
            var pos = (Vector3)movementSpline.Spline.EvaluatePosition(splinePosition) + movementSpline.transform.position;
            transform.position = pos;
            
            Vector3 direction = movementSpline.EvaluateTangent(splinePosition);
            direction = direction.normalized;
            animator.SetFloat("LeftRight", direction.x > 0 ? 1.0f : -1.0f);
            splinePosition += movementSpeed * Time.deltaTime * 1/movementSpline.Spline.GetLength();
            while (splinePosition >= 1)
            {
                splinePosition -= 1;
            }
        }
        else
        {
            animator.SetFloat("LeftRight", 0.0f);
        }
    }
}
