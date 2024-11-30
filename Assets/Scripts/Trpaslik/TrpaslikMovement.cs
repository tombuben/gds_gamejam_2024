using System;
using UnityEngine;
using UnityEngine.Splines;

public class TrpaslikMovement : MonoBehaviour
{
    public SplineContainer movementSpline;
    public float movementSpeed = 0.1f;
    public float splinePosition;
    public bool movingForward;

    private float zDepth;
    private void Start()
    {
        GlobalManager.Instance.PlayerSpawned += PlayerSpawned;
    }

    private void PlayerSpawned(CharacterControllerSwitcher character)
    {
        zDepth = character.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = movementSpline.EvaluatePosition(splinePosition);
        pos.z = zDepth;
        transform.position = pos;

        if (movingForward)
        {
            splinePosition += movementSpeed * Time.deltaTime * 1/movementSpline.Spline.GetLength();
            if (splinePosition >= 1)
            {
                splinePosition = 1;
                movingForward = false;
            }
        }
        else
        {
            splinePosition -= movementSpeed * Time.deltaTime * 1/movementSpline.Spline.GetLength();
            if (splinePosition <= 0)
            {
                splinePosition = 0;
                movingForward = true;
            }
        }
    }
}
