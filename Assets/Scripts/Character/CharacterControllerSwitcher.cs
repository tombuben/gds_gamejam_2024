using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CharacterControllerSwitcher : MonoBehaviour
{
    private CharacterController2D controller2D;
    private CharacterController3D controller3D;
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IEnumerator Start()
    {
        controller2D = GetComponent<CharacterController2D>();
        controller3D = GetComponent<CharacterController3D>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        
        controller3D.enabled = false;
        controller2D.enabled = true;
        GlobalManager.Instance.OnTogglePerspective += TogglePerspective;
        
        GlobalManager.Instance.OnDialogShown += DialogShown;
        GlobalManager.Instance.OnDialogClosed += DialogHidden;
        yield return null;
        GlobalManager.Instance.PlayerSpawned?.Invoke(this);
    }

    private void DialogShown()
    {
        controller3D.enabled = false;
        controller2D.enabled = false;
    }
    
    private void DialogHidden()
    {
        TogglePerspective(GlobalManager.Instance.is3D);
    }


    private void TogglePerspective(bool is3D)
    {
        controller3D.enabled = is3D;
        controller2D.enabled = !is3D;
        rb.constraints = is3D ? RigidbodyConstraints.FreezePositionZ : RigidbodyConstraints.None;
    }

}
