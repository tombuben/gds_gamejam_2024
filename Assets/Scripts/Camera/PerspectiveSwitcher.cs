using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MatrixBlender))]
public class PerspectiveSwitcher : MonoBehaviour
{
    private Matrix4x4   ortho,
                        perspective;
    public float        fov     = 60f,
                        near    = .3f,
                        far     = 1000f,
                        orthographicSize = 20f;
    private float       aspect;
    private MatrixBlender blender;
    private bool        orthoOn;

    void Start()
    {
        aspect = (float) Screen.width / (float) Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        GetComponent<Camera>().projectionMatrix = ortho;
        orthoOn = true;
        blender = (MatrixBlender) GetComponent(typeof(MatrixBlender));

        GlobalManager.Instance.TogglePerspective += HandleTogglePerspective;
    }

    void OnDestroy(){

        GlobalManager.Instance.TogglePerspective -= HandleTogglePerspective;
    }

    void HandleTogglePerspective(bool is3D){
        orthoOn = !orthoOn;
        if (!is3D)
            blender.BlendToMatrix(ortho, 1f);
        else
            blender.BlendToMatrix(perspective, 1f);
    }
}