using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PostprocessOn3D : MonoBehaviour
{
    public float duration = 0.5f;
    
    private Volume volume;
    private DepthOfField dofComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = GetComponent<Volume>();
        
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            dofComponent = tmp;
        }
        GlobalManager.Instance.TogglePerspectiveFinished += TogglePerspectiveFinished;
        GlobalManager.Instance.OnTogglePerspective += TogglePerspective;
    }

    private void TogglePerspectiveFinished()
    {
        if (GlobalManager.Instance.is3D)
        {
            DOTween.To(()=>volume.weight,weight=>volume.weight = weight,1.0f,duration);
        }
        else
        {
            DOTween.To(()=>volume.weight,weight=>volume.weight = weight,0.0f,duration);
        }
    }
    
    private void TogglePerspective(bool is3D)
    {
        if (!is3D)
        {
            DOTween.To(()=>volume.weight,weight=>volume.weight = weight,0.0f,duration);
        }
    }
}
