using UnityEngine;

public class CharacterControllerSwitcher : MonoBehaviour
{
    private CharacterController2D controller2D;
    private CharacterController3D controller3D;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller2D = GetComponent<CharacterController2D>();
        controller3D = GetComponent<CharacterController3D>();
        
        controller3D.enabled = false;
        controller2D.enabled = true;
        GlobalManager.Instance.togglePerspective += TogglePerspective;
    }

    private void TogglePerspective(bool is3D)
    {
        if (is3D)
        {
            controller3D.enabled = true;
            controller2D.enabled = false;
        }
    }

}
