using System;
using UnityEngine;
using UnityEngine.Splines;

public class TrpaslikPathRunner : MonoBehaviour
{
    public SplineContainer splines;
    public int NextSplineIndex = 0;
    public bool isRunning = false;
    public float splinePosition = 0;
    public float speed = 15;
    
    private Animator animator;
    private DialogInstance dialog;
    
    public Spline CurrentSpline => splines[NextSplineIndex];

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        dialog = GetComponentInChildren<DialogInstance>();
        dialog.enabled = false;
        
        var pos = (Vector3)CurrentSpline.EvaluatePosition(splinePosition) + splines.transform.position;
        transform.position = pos;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (NextSplineIndex < splines.Splines.Count)
        {
            isRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            var pos = (Vector3)CurrentSpline.EvaluatePosition(splinePosition) + splines.transform.position;
            transform.position = pos;

            splinePosition += speed * Time.deltaTime * 1/CurrentSpline.GetLength();

            Vector3 direction = CurrentSpline.EvaluateTangent(splinePosition);
            direction = direction.normalized;
            animator.SetFloat("LeftRight", direction.x > 0 ? 1.0f : -1.0f);

            if (splinePosition > 1)
            {
                NextSplineIndex += 1;
                splinePosition = 0;
                isRunning = false;
                
                if (NextSplineIndex >= splines.Splines.Count)
                {
                    dialog.enabled = true;
                }
            }
        }
        else
        {
            animator.SetFloat("LeftRight", 0.0f);
        }
    }
}
