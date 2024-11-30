using System.Collections;
using UnityEngine;

public class CharacterControllerSwitcher : MonoBehaviour
{
    private CharacterController2D controller2D;
    private CharacterController3D controller3D;
    private CharacterController controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IEnumerator Start()
    {
        controller2D = GetComponent<CharacterController2D>();
        controller3D = GetComponent<CharacterController3D>();
        controller = GetComponent<CharacterController>();
        
        controller3D.enabled = false;
        controller2D.enabled = true;
        GlobalManager.Instance.OnTogglePerspective += TogglePerspective;
        yield return null;
        GlobalManager.Instance.PlayerSpawned?.Invoke(this);
    }

    private void TogglePerspective(bool is3D)
    {
        controller3D.enabled = is3D;
        controller2D.enabled = !is3D;
        controller.attachedRigidbody.constraints = is3D ? RigidbodyConstraints.FreezePositionZ : RigidbodyConstraints.None;
    }

}
