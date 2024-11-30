using DG.Tweening;
using UnityEngine;

public class PerspectiveRotate : MonoBehaviour
{
    public float cameraRotation = 20f;
    public float transitionSpeed = 0.2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalManager.Instance.togglePerspective += HandleTogglePerspective;
    }
    
    void HandleTogglePerspective()
    {
        if (GlobalManager.Instance.is3D)
        {
            transform.DORotate(new Vector3(cameraRotation, 0f, 0f), transitionSpeed);
        }
        else
        {
            transform.DORotate(new Vector3(0, 0f, 0f), transitionSpeed);
        }
    }
}
